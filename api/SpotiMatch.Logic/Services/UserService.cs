using System;
using System.Collections.Generic;
using System.Text;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Database.Repositories.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using AutoMapper;
using SpotiMatch.Shared.Dtos;
using SpotiMatch.Logic.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SpotiMatch.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper Mapper;
        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            Mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
        {
            List<UserDto> users = (await UserRepository
                .GetUsers()
                .ToListAsync(cancellationToken))
                .Select(user => user.ToDto(Mapper))
                .ToList();

            return users;
        }

        public async Task<UserDto> GetUser(int id, CancellationToken cancellationToken)
        {
            return (await UserRepository.GetUser(id, cancellationToken))
                .ToDto(Mapper);
        }

        public async Task<UserDto> AddUser(UserDto user, CancellationToken cancellationToken)
        {
            return (await UserRepository.AddUser(user.ToEntity(Mapper), cancellationToken))
                .ToDto(Mapper);
        }

        public async Task<UserDto> UpdateUser(UserDto user, CancellationToken cancellationToken)
        {
            return (await UserRepository.UpdateUser(user.ToEntity(Mapper), cancellationToken))
                .ToDto(Mapper);
        }

        public async Task<bool> DeleteUser(int id, CancellationToken cancellationToken)
        {
            return await UserRepository.DeleteUser(id, cancellationToken);
        }
    }
}
