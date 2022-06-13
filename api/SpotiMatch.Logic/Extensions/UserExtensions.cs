using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using Entity = SpotiMatch.Database.Entities.User;
using Model = SpotiMatch.Shared.Models.User;
using Dto = SpotiMatch.Shared.Dtos.UserDto;

namespace SpotiMatch.Logic.Extensions
{
    public static class UserExtensions
    {
        public static Model ToModel(this Entity entity, IMapper mapper)
        {
            return mapper.Map<Model>(entity);
        }

        public static Entity ToEntity(this Model model, IMapper mapper)
        {
            return mapper.Map<Entity>(model);
        }

        public static Dto ToDto(this Model model, IMapper mapper)
        {
            return mapper.Map<Dto>(model);
        }

        public static Model ToModel(this Dto dto, IMapper mapper)
        {
            return mapper.Map<Model>(dto);
        }

        public static Entity ToEntity(this Dto dto, IMapper mapper)
        {
            return mapper.Map<Entity>(dto.ToModel(mapper));
        }

        public static Dto ToDto(this Entity entity, IMapper mapper)
        {
            return mapper.Map<Dto>(entity.ToModel(mapper));
        }
    }
}
