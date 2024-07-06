using Server.Domain.Entities;
using Server.Shared.Dtos;

namespace Server.Shared.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
    Task<List<UserDto>> ListUsersExcludingId(Guid Id);
}
