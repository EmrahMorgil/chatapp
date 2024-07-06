using Dapper;
using Server.Application.Dto;
using Server.Application.Interfaces;
using Server.Domain.Entities;
using Server.Persistence.Context;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace Server.Persistence.Repositories;

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
        var parameters = new DynamicParameters();
        parameters.Add("Email", pEmail);

        using (var connection = _dbContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(query, parameters) ?? null!;
        }
    }

    public async Task<List<UserDto>> ListUsersExcludingId(Guid pId)
    {
        var query = @"select Id,Name,Image from users where Id <> @pId";
        var parameters = new DynamicParameters();
        parameters.Add("Id", pId);

        using (var connection = _dbContext.CreateConnection())
        {
            var users = await connection.QueryAsync<UserDto>(query, parameters);
            return users.ToList();
        }
    }

}
