using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public Task<Message> AddMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetAllMessages(GetAllMessagesCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
