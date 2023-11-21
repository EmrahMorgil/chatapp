using Server.Application.Features.Commands.Login;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginCommand> LoginUser(LoginCommand user)
        {
            throw new NotImplementedException();
        }
    }
}
