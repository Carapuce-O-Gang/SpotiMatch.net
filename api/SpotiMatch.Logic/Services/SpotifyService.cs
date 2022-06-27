using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Shared.Dtos.Spotify;
using SpotiMatch.Logic.Extensions;

namespace SpotiMatch.Logic.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IConfiguration Configuration;

        private readonly HttpClient Client;

        public SpotifyService(IConfiguration configuration)
        {
            Configuration = configuration;
            Client = new HttpClient();
        }

        public async Task<TokenDto> GetAccessToken(string authorizationToken, CancellationToken cancellationToken)
        {
            string redirectUri = Configuration.GetValue<string>("Spotify:RedirectUri");

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authorizationToken),
                new KeyValuePair<string, string>("redirect_uri", redirectUri)
            });
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Configuration.GetValue<string>("Spotify:ClientEncodedKey"));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await Client.PostAsync("https://accounts.spotify.com/api/token", content, cancellationToken);
            return await response.Deserialize<TokenDto>();
        }

        public async Task<TokenDto> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            string redirectUri = Configuration.GetValue<string>("Spotify:RedirectUri");

            FormUrlEncodedContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "refresh_token"),
                new KeyValuePair<string, string>("refresh_token", refreshToken)
            });
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Configuration.GetValue<string>("Spotify:ClientEncodedKey"));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await Client.PostAsync("https://accounts.spotify.com/api/token", content, cancellationToken);
            return await response.Deserialize<TokenDto>();
        }

        public async Task<ProfileDto> GetProfile(string accessToken, CancellationToken cancellationToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await Client.GetAsync("https://api.spotify.com/v1/me", cancellationToken);
            return await response.Deserialize<ProfileDto>();
        }
    }
}
