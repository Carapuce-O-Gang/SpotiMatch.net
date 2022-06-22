using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos.Spotify;

namespace SpotiMatch.Logic.Services.Interfaces
{
    public interface ISpotifyService
    {
        Task<TokenDto> GetAccessToken(string authorizationToken, CancellationToken cancellationToken = default);
        Task<TokenDto> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        Task<ProfileDto> GetProfile(string accessToken, CancellationToken cancellationToken = default);
    }
}
