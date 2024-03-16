using Azure;
using Dapper;
using Server.Application.Features.Message.Queries;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Server.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DapperContext _dbContext;

        public MessageRepository(DapperContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Message entity)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = @"insert into tbl_message (id,created_date,sender_id,content,room) values (@id,@created_date,@sender_id,@content,@room)";

            var parameters = new DynamicParameters();
            parameters.Add("id", entity.Id, DbType.Guid);
            parameters.Add("created_date", entity.CreatedDate, DbType.DateTime);
            parameters.Add("sender_id", entity.SenderId, DbType.Guid);
            parameters.Add("content", entity.Content, DbType.String);
            parameters.Add("room", entity.Room, DbType.String);

            using (var connection = _dbContext.CreateConnection())
            {
                return await connection.ExecuteAsync(query, parameters) > 0;
            }
        }

        public async Task<List<Message>> List()
        {
            var query = "select * from tbl_message";

            using (var connection = _dbContext.CreateConnection())
            {
                var messageList = await connection.QueryAsync<Message>(query);
                return messageList.OrderBy((w)=>w.CreatedDate).ToList();
            }
        }

        public Task<bool> Delete(Message entity)
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> Update(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
