using Server.Application.Features.Commands.Login;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<LoginCommand> LoginUser(LoginCommand user);
    }
}
