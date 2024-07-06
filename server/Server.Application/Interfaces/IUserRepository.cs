using Server.Application.Dto;
using Server.Domain.Entities;

namespace Server.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
    Task<List<UserDto>> ListUsersExcludingId(Guid Id);
}
