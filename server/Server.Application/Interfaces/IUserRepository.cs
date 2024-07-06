using Server.Domain.Entities;

namespace Server.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
}
