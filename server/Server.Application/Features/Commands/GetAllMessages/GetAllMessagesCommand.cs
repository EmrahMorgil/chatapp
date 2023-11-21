using AutoMapper;
using MediatR;
using Server.Application.Features.Commands.CreateUser;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.GetAllMessages
{
    public class GetAllMessagesCommand: IRequest<List<Message>>
    {
        public string? takerId { get; set; }
        public string? senderId { get; set; }


        public class GetAllMessagesHandler : IRequestHandler<GetAllMessagesCommand, List<Message>>
        {
            IMessageRepository _messageRepository;
            private readonly IMapper _mapper;

            public GetAllMessagesHandler(IMessageRepository messageRepository, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _mapper = mapper;
            }

            public async Task<List<Message>> Handle(GetAllMessagesCommand request, CancellationToken cancellationToken)
            {
                var messages = await _messageRepository.GetAllMessages(request);
                return messages;
            }

        }
    }
}
