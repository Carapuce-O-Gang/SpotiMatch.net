using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            // Refresh spotify access token
            if (user.RefreshToken != null)
            {
                TokenDto accessToken = await SpotifyService.RefreshToken(user.RefreshToken, cancellationToken);

                if (accessToken == null || accessToken.AccessToken == null)
                {
                    return null;
                }

                user.AccessToken = accessToken.AccessToken;
            }

            await UserRepository.UpdateUser(user, cancellationToken);

            // Send api authentication token
            string signingKey = Configuration.GetValue<string>("AppConfiguration:SigningKey");

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                SecurityAlgorithms.HmacSha256);

            string issuer = Configuration.GetValue<string>("AppConfiguration:AppName");
            int tokenValidity = Configuration.GetValue<int>("AppConfiguration:AuthenticationTokenValidity");

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));

            JwtSecurityToken authToken = new JwtSecurityToken(
                issuer: issuer,
                signingCredentials: credentials,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(tokenValidity),
                claims: claims);

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

            TokenDto accessToken = await SpotifyService.GetAccessToken(register.AuthorizationToken, cancellationToken);

            if (accessToken == null)
            {
                return null;
            }

            List<Image> images = new List<Image>();
            Image profileImage = await GetProfilePicture(accessToken.AccessToken, cancellationToken);
            if (profileImage != null)
            {
                images.Add(profileImage);
            }

            User userToAdd = new User
            {
                Name = register.Name,
                DisplayName = register.DisplayName,
                Email = register.Email,
                Password = Crypt.BCrypt.HashPassword(register.Password),
                AuthorizationToken = register.AuthorizationToken,
                AccessToken = accessToken.AccessToken,
                RefreshToken = accessToken.RefreshToken,
                Images = images
            };

            return (await UserRepository.AddUser(userToAdd, cancellationToken))
                .ToDto(Mapper);
        }

        private async Task<Image> GetProfilePicture(string accessToken, CancellationToken cancellationToken)
        {
            ProfileDto profile = await SpotifyService.GetProfile(accessToken, cancellationToken);
            
            if (profile.Images[0] == null)
            {
                return null;
            }

            return profile.Images[0].ToEntity(Mapper);
        }
    }
}
