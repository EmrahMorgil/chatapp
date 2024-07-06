using Server.Domain.Entities;
using Server.Shared.Dtos;

namespace Server.Shared.Interfaces;

public interface IMessageRepository : IGenericRepository<Message>
{
    Task<List<MessageDto>> GetFilteredMessages(string Room);
}
