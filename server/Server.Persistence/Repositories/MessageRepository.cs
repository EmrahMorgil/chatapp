using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using Server.Persistence.Context;


namespace Server.Persistence.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly DapperContext _dbContext;

        public MessageRepository(DapperContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
