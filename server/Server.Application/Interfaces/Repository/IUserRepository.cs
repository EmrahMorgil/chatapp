using Server.Domain.Entities;

namespace Server.Application.Interfaces.Repository;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
}
