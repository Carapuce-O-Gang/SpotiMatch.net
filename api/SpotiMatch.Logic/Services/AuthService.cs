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
using SpotiMatch.Shared.Dtos.Spotify;
using SpotiMatch.Database.Entities;
using SpotiMatch.Database.Repositories.Interfaces;

namespace SpotiMatch.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration Configuration;

        private readonly IUserRepository UserRepository;

        private readonly ISpotifyService SpotifyService;

        private readonly IMapper Mapper;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, ISpotifyService spotifyService, IMapper mapper)
        {
            Configuration = configuration;
            UserRepository = userRepository;
            SpotifyService = spotifyService;
            Mapper = mapper;
        }

        public async Task<AuthDto> Login(LoginDto login, CancellationToken cancellationToken)
        {
            // Verify credentials
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

            // Generate spotify access token
            TokenDto accessToken;
            if (user.RefreshToken == null)
            {
                accessToken = await SpotifyService.GetAccessToken(user.AuthorizationToken, cancellationToken);
                user.AccessToken = accessToken.AccessToken;
                user.RefreshToken = accessToken.RefreshToken;
            } else 
            {
                accessToken = await SpotifyService.RefreshToken(user.RefreshToken, cancellationToken);
                user.AccessToken = accessToken.AccessToken;
            }
            
            if (accessToken == null || accessToken.AccessToken == null)
            {
                return null;
            }

            await UserRepository.UpdateUser(user, cancellationToken);

            // Send api authentication token
            string signingKey = Configuration.GetValue<string>("AppConfiguration:SigningKey");

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                SecurityAlgorithms.HmacSha256);

            string issuer = Configuration.GetValue<string>("AppConfiguration:AppName");
            int tokenValidity = Configuration.GetValue<int>("AppConfiguration:AuthenticationTokenValidity");

            JwtSecurityToken authToken = new JwtSecurityToken(
                issuer: issuer,
                signingCredentials: credentials,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(tokenValidity));

            return new AuthDto
            {
                TokenType = JwtBearerDefaults.AuthenticationScheme,
                Token = new JwtSecurityTokenHandler().WriteToken(authToken),
                ExpiredDate = authToken.Payload.ValidTo
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
