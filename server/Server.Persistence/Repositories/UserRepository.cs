using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Identity.Client;
using Server.Application.Dto;
using Server.Application.Features.User.Queries;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Context;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DapperContext _dbContext;

        public UserRepository(DapperContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByEmail(string pEmail)
        {
            var query = @"select * from users where email = @email";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { email = pEmail });
            }
        }
    }
}
