using Server.Application.Dto;
using Server.Domain.Entities;

namespace Server.Application.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<List<MessageDto>> GetFilteredMessages(string Room);
}
