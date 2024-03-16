using Server.Application.Dto;
using Server.Application.Features.User.Commands;
using Server.Application.Features.User.Queries;
using Server.Application.Handlers.User.Commands;
using Server.Application.Wrappers;
using Server.Domain.Entities;


namespace Server.Application.Interfaces.Repository
{
    public interface IUserRepository: IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
