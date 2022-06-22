using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using SpotiMatch.Shared.Dtos;
using SpotiMatch.Shared.Models;

namespace SpotiMatch.Logic.Mappings
{
    public class DtosProfile : Profile
    {
        public DtosProfile()
        {
            CreateUserDtoMap();
        }

        private void CreateUserDtoMap()
        {
            CreateMap<UserDto, User>()
                .ForMember(m => m.Id, o => o.MapFrom(d => d.Id))
                .ForMember(m => m.Name, o => o.MapFrom(d => d.Name))
                .ForMember(m => m.DisplayName, o => o.MapFrom(d => d.DisplayName))
                .ForMember(m => m.Email, o => o.MapFrom(d => d.Email))
                .ForMember(m => m.AccessToken, o => o.Ignore())
                .ForMember(m => m.ProfilePicture, o => o.MapFrom(d => d.ProfilePicture))
                .ForMember(m => m.CreatedOn, o => o.MapFrom(d => d.CreatedOn))
                .ReverseMap();
        }
    }
}
