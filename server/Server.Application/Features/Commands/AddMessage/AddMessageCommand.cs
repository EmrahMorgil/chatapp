using AutoMapper;
using MediatR;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.AddMessage
{
    public class AddMessageCommand: IRequest<Message>
    {
        public string? message { get; set; }
        public string? room { get; set; }

        public class AddMessageCommandHandler: IRequestHandler<AddMessageCommand, Message>
        {
            IMessageRepository _messageRepository;
            private readonly IMapper _mapper;

            public AddMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _mapper = mapper;
            }

            public async Task<Message> Handle(AddMessageCommand request, CancellationToken cancellationToken)
            {
                var message = _mapper.Map<Domain.Entities.Message>(request);
                message.id = Guid.NewGuid();
                message.createdDate = DateTime.Now;
                await _messageRepository.AddMessage(message);
                return message;
            }

        }
    }
}
