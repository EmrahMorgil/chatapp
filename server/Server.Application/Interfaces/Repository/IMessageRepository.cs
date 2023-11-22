using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Wrappers;
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
        Task<bool> AddMessage(Message message);
        Task<MessagesResponse> GetAllMessages(GetAllMessagesCommand request);
    }
}
