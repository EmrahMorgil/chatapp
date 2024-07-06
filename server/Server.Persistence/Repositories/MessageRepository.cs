using Dapper;
using Server.Domain.Entities;
using Server.Persistence.Context;
using Server.Shared.Dtos;
using Server.Shared.Interfaces;

namespace Server.Persistence.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    private readonly DapperContext _dbContext;

    public MessageRepository(DapperContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<MessageDto>> GetFilteredMessages(string pRoom)
    {
        var query = @"SELECT m.Id,m.CreatedDate,m.Content,m.Room,u.Id AS UserId,u.Name AS UserName,u.Image AS UserImage
                      from Messages m
                      INNER JOIN Users u
                      ON m.UserId = u.Id
                      WHERE Room = @pRoom";
        var parameters = new DynamicParameters();
        parameters.Add("pRoom", pRoom);

        using (var connection = _dbContext.CreateConnection())
        {
            var result = await connection.QueryAsync<MessageDto>(query, parameters);
            return result.ToList();
        }
    }
}
