using Dapper;
using Server.Domain.Entities;
using Server.Persistence.Context;
using Server.Shared.Dtos;
using Server.Shared.Interfaces;

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
        var query = @"select * from users where email = @pEmail";
        var parameters = new DynamicParameters();
        parameters.Add("pEmail", pEmail);

        using (var connection = _dbContext.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<User>(query, parameters) ?? null!;
        }
    }

    public async Task<List<UserDto>> ListUsersExcludingId(Guid pId)
    {
        var query = @"select Id,Name,Image from users where Id <> @pId";
        var parameters = new DynamicParameters();
        parameters.Add("pId", pId);

        using (var connection = _dbContext.CreateConnection())
        {
            var users = await connection.QueryAsync<UserDto>(query, parameters);
            return users.ToList();
        }
    }

}
