using Dapper;
using Server.Application.Features.Commands.Login;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Domain.Entities;
using Server.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Server.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dbContext;
        public UserRepository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CreateUser(User user)
        {
            var query = $"INSERT INTO [User] ({RepositoryHelper.GetInsertFields<User>()}) VALUES ({RepositoryHelper.GetInsertFieldParams<User>()})";
            var emailControl = "SELECT * FROM [User] WHERE email = @email";

            using (var connection = _dbContext.CreateConnection())
            {
                var getUser = await connection.QueryFirstOrDefaultAsync<User>(emailControl, new { email = user.email });

                if (getUser != null)
                    return false;
                else
                {
                    var userControl = await connection.ExecuteAsync(query, user);
                    if (userControl != null)
                        return true;
                    else
                        return false;
                }
            }
        }

        public async Task<bool> LoginUser(LoginCommand user)
        {
            var query = "SELECT * FROM [User] WHERE email = @email";

            using (var connection = _dbContext.CreateConnection())
            {
                var userControl = await connection.QueryFirstOrDefaultAsync<User>(query, new { email = user.email});
                if (userControl != null)
                {
                   return Encryption.VerifyPassword(user.password, userControl.password);
                }
                return false;
            }
        }

       
    }
}
