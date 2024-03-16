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
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _dbContext;

        public UserRepository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(User entity)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = @"insert into tbl_user (id,created_date,email,name,password,image) values (@id,@created_date,@email,@name,@password,@image)";

            var parameters = new DynamicParameters();
            parameters.Add("id", entity.Id, DbType.Guid);
            parameters.Add("created_date", entity.CreatedDate, DbType.DateTime);
            parameters.Add("email", entity.Email, DbType.String);
            parameters.Add("name", entity.Name, DbType.String);
            parameters.Add("password", entity.Password, DbType.String);
            parameters.Add("image", entity.Image, DbType.String);

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters) > 0;
            }
        }

        public async Task<List<User>> List()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = "select * from tbl_user";

            using (var connection = _dbContext.CreateConnection())
            {
                var userList = await connection.QueryAsync<User>(query);
                return userList.OrderBy((u)=>u.CreatedDate).ToList();
            }
        }

        public async Task<User> GetByEmail(string pEmail)
        {
            var query = @"select * from tbl_user where email = @email";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>(query, new { email = pEmail });
            }
        }
        public async Task<bool> Update(User entity)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = @"update tbl_user set name=@name,email=@email,password=@password,image=@image WHERE id=@id;";

            var parameters = new DynamicParameters();
            parameters.Add("id", entity.Id, DbType.Guid);
            parameters.Add("name", entity.Name, DbType.String);
            parameters.Add("email", entity.Email, DbType.String);
            parameters.Add("password", entity.Password, DbType.String);
            parameters.Add("image", entity.Image, DbType.String);


            using(var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters) > 0;
            }
        }
        public Task<bool> Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid pId)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = @"select * from tbl_user where id = @id";

            using (var connection = _dbContext.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { id = pId });
                if (user == null)
                {
                    throw new Exception();
                }
                else
                {
                    return user;
                }
            }
        }
    }
}
