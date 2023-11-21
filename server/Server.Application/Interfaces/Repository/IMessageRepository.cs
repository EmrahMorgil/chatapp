using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces.Repository
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<List<Message>> GetAllMessages();
    }
}
