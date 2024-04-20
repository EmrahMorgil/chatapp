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
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly DapperContext _dbContext;

        public MessageRepository(DapperContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
