using Dapper;
using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
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
            var query = $"INSERT INTO [Message] (senderId, message, room, createdDate) VALUES (@senderId, @message, @room, @createdDate)";
            var control = false;
            using (var connection = _dbContext.CreateConnection())
            {
                var messages = await connection.ExecuteAsync(query, message);
                if (messages != null)
                    control = true;
            }
            return control;
        }

        public async Task<MessagesResponse> GetAllMessages(GetAllMessagesCommand request)
        {
            var query = $"SELECT * FROM [Message] WHERE room IN ('{request.takerId}', '{request.senderId}')";

            using (var connection = _dbContext.CreateConnection())
            {
                var response = new MessagesResponse();
                var getMessages = await connection.QueryAsync<Message>(query);
                var messages = getMessages.ToList();
                response.messages = messages;
                if (messages.Count < 1)
                {
                    response.room = request.takerId ?? default(Guid);
                }
                else
                {
                    response.room = (Guid)messages[0].room;
                }
                return response;
            }
        }
    }
}
