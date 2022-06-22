using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using SpotiMatch.Shared.Dtos.Spotify;
using SpotiMatch.Database.Entities;

namespace SpotiMatch.Logic.Mappings
{
    public class SpotifyProfile : Profile
    {
        public SpotifyProfile()
        {
            CreateImageDtoMap();
        }

        private void CreateImageDtoMap()
        {
            CreateMap<ImageDto, Image>()
                .ForMember(e => e.Id, o => o.Ignore())
                .ForMember(e => e.Url, o => o.MapFrom(d => d.Url))
                .ForMember(e => e.Height, o => o.MapFrom(d => d.Height))
                .ForMember(e => e.Width, o => o.MapFrom(d => d.Width))
                .ReverseMap();
        }
    }
}
