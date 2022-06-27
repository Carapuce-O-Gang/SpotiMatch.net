using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using AutoMapper;
using SpotiMatch.Database.Entities;
using SpotiMatch.Shared.Dtos.Spotify;

namespace SpotiMatch.Logic.Extensions
{
    public static class SpotifyExtensions
    {
        public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            T deserializedObject = JsonConvert.DeserializeObject<T>(responseContent, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            return deserializedObject;
        }

        public static Image ToEntity(this ImageDto image, IMapper mapper)
        {
            return mapper.Map<Image>(image);
        }
    }
}
