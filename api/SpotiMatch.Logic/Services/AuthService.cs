using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Crypt = BCrypt.Net;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Logic.Extensions;
using SpotiMatch.Shared.Dtos;
using SpotiMatch.Database.Entities;
using SpotiMatch.Database.Repositories.Interfaces;

namespace SpotiMatch.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration Configuration;

        private readonly IUserRepository UserRepository;

        private readonly IMapper Mapper;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IMapper mapper)
        {
            Configuration = configuration;
            UserRepository = userRepository;
            Mapper = mapper;
        }

        public async Task<AuthDto> Login(LoginDto login, CancellationToken cancellationToken)
        {
            User user = (await UserRepository
                .GetUsers()
                .ToListAsync(cancellationToken))
                .SingleOrDefault(u => u.Name == login.Username);

            if (user == null)
            {
                return null;
            }

            bool isLoginValid = Crypt.BCrypt.Verify(login.Password, user.Password);

            if (!isLoginValid)
            {
                return null;
            }

            string signingKey = Configuration.GetValue<string>("AppConfiguration:SigningKey");

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                SecurityAlgorithms.HmacSha256);

            string issuer = Configuration.GetValue<string>("AppConfiguration:AppName");
            int tokenValidity = Configuration.GetValue<int>("AppConfiguration:AuthenticationTokenValidity");

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                signingCredentials: credentials,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(tokenValidity)
                );

            return new AuthDto
            {
                TokenType = JwtBearerDefaults.AuthenticationScheme,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredDate = token.Payload.ValidTo
            };
        }

        public async Task<UserDto> Register(RegisterDto register, CancellationToken cancellationToken)
        {
            if (register.Password == null || register.Password != register.PasswordConfirmation)
            {
                return null;
            }

            User userToAdd = new User
            {
                Name = register.Name,
                DisplayName = register.DisplayName,
                Email = register.Email,
                Password = Crypt.BCrypt.HashPassword(register.Password),
                AuthorizationToken = register.AuthorizationToken
            };

            return (await UserRepository.AddUser(userToAdd, cancellationToken))
                .ToDto(Mapper);
        }
    }
}
