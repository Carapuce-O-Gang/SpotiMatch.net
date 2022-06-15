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
            this.CreateUserDtoMap();
        }

        private void CreateUserDtoMap()
        {
            CreateMap<UserDto, User>()
                .ForMember(m => m.Id, o => o.MapFrom(d => d.Id))
                .ForMember(m => m.Name, o => o.MapFrom(d => d.Name))
                .ForMember(m => m.DisplayName, o => o.MapFrom(d => d.DisplayName))
                .ForMember(m => m.Email, o => o.MapFrom(d => d.Email))
                .ForMember(m => m.Password, o => o.Ignore())
                .ForMember(m => m.CreatedOn, o => o.MapFrom(d => d.CreatedOn))
                .ReverseMap()
                .ForMember(d => d.Password, o => o.MapFrom(m => m.Password));
        }
    }
}
