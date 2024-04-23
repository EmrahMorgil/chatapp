using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Identity.Client;
using Server.Application.Dto;
using Server.Application.Features.User.Queries;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Persistence.Context;
using Server.Persistence.Helpers;
using System.Data;

namespace Server.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DapperContext _dbContext;

        public GenericRepository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(T entity)
        {
            var tableName = RepositoryHelper.GetTableName<T>();
            var properties = RepositoryHelper.GetPropertyNames<T>().ToList();
            var parameters = properties.Select(p => "@" + p).ToList();
            var query = $"insert into {tableName} ({string.Join(",", properties)}) values ({string.Join(",", parameters)})";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, entity) > 0;
            }
        }

        public async Task<T> GetById(Guid id)
        {
            var tableName = RepositoryHelper.GetTableName<T>();
            var query = $"select * from {tableName} where Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                var entity = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
                if (entity == null)
                {
                    throw new Exception();
                }
                return entity;
            }
        }

        public async Task<List<T>> List()
        {
            var tableName = RepositoryHelper.GetTableName<T>();
            var query = $"select * from {tableName}";

            using (var connection = _dbContext.CreateConnection())
            {
                var entityList = await connection.QueryAsync<T>(query);
                return entityList.ToList();
            }
        }

        public async Task<bool> Update(T entity)
        {
            var tableName = RepositoryHelper.GetTableName<T>();
            var properties = RepositoryHelper.GetPropertyNames<T>().Where(p => p != "Id").ToList();
            var setStatements = properties.Select(p => $"{p} = @{p}");
            var query = $"update {tableName} set {string.Join(",", setStatements)} WHERE Id = @Id";

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, entity) > 0;
            }
        }
        public Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        
    }
}