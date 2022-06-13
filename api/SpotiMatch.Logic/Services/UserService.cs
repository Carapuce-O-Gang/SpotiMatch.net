using System;
using System.Collections.Generic;
using System.Text;
using SpotiMatch.Logic.Services.Interfaces;
using SpotiMatch.Database.Repositories.Interfaces;
using System.Threading.Tasks;
using SpotiMatch.Shared.Dtos;
using System.Threading;
using AutoMapper;

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

        public Task<IEnumerable<UserDto>> GetUsers(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUser(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> AddUser(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUser(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
