using Dapper;
using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using Server.Persistence.Context;
using System;
using System.Collections.Generic;
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
        public async Task<bool> AddMessage(Message message)
        {
            var query = $"INSERT INTO [{typeof(Message).Name}] ({RepositoryHelper.GetInsertFields<Message>()}) VALUES ({RepositoryHelper.GetInsertFieldParams<Message>()})";
            var control = false;
            using (var connection = _dbContext.CreateConnection())
            {
                var products = await connection.ExecuteAsync(query, message);
                if (products != null)
                    control = true;
            }
            return control;
        }

        public async Task<List<Message>> GetAllMessages(GetAllMessagesCommand request)
        {
            var queryOne = $"SELECT * FROM [Message] WHERE room = {request.takerId}";
            var queryTwo = $"SELECT * FROM [Message] WHERE room = {request.senderId}";

            using (var connection = _dbContext.CreateConnection())
            {
                var messagesOne = await connection.QueryAsync<Message>(queryOne);
                var messagesTwo = await connection.QueryAsync<Message>(queryTwo);

                if (messagesOne != null)
                    return messagesOne.ToList();
                else if(messagesTwo != null) return messagesTwo.ToList();
                else
                {
                    return messagesOne.ToList();
                    //Burada Hub dan yeni oda oluştur.
                }
            }
        }
    }
}
