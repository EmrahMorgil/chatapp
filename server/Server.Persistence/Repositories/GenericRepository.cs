using Dapper;
using Server.Persistence.Context;
using Server.Persistence.Helpers;
using Server.Shared.Interfaces;
using System.Data;

namespace Server.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DapperContext _dbContext;

    public GenericRepository(DapperContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Create(T pEntity)
    {
        var tableName = RepositoryHelper.GetTableName<T>();
        var properties = RepositoryHelper.GetPropertyNames<T>().ToList();
        var parameters = properties.Select(p => "@" + p).ToList();
        var query = $"insert into {tableName} ({string.Join(",", properties)}) values ({string.Join(",", parameters)})";

        using (var connection = _dbContext.CreateConnection())
        {
            return await connection.ExecuteAsync(query, pEntity) > 0;
        }
    }

    public async Task<T> GetById(Guid pId)
    {
        var tableName = RepositoryHelper.GetTableName<T>();
        var query = $"select * from {tableName} where Id = @Id";
        var parameters = new DynamicParameters();
        parameters.Add("Id", pId);

        using (var connection = _dbContext.CreateConnection())
        {
            var entity = await connection.QueryFirstOrDefaultAsync<T>(query, parameters);
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