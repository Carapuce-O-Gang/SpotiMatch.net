using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using Entities = SpotiMatch.Database.Entities;
using Models = SpotiMatch.Shared.Models;

namespace SpotiMatch.Logic.Mappings
{
    public class EntitiesProfile : Profile
    {
        public EntitiesProfile()
        {
            CreateUserMap();
        }

        private void CreateUserMap()
        {
            CreateMap<Entities.User, Models.User>()
                .ForMember(m => m.Id, o => o.MapFrom(e => e.Id))
                .ForMember(m => m.Name, o => o.MapFrom(e => e.Name))
                .ForMember(m => m.DisplayName, o => o.MapFrom(e => e.DisplayName))
                .ForMember(m => m.Email, o => o.MapFrom(e => e.Email))
                .ForMember(m => m.AuthorizationToken, o => o.MapFrom(e => e.AuthorizationToken))
                .ForMember(m => m.AccessToken, o => o.MapFrom(e => e.AccessToken))
                .ForMember(m => m.CreatedOn, o => o.MapFrom(e => e.CreatedOn))
                .ReverseMap()
                .ForMember(e => e.Password, o => o.Ignore());
        }
    }
}
