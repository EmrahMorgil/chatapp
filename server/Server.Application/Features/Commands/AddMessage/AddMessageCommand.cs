using AutoMapper;
using MediatR;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.AddMessage
{
    public class AddMessageCommand: IRequest<BaseResponse<Message>>
    {
        public string? senderId { get; set; }
        public string? message { get; set; }
        public string? room { get; set; }

        public class AddMessageCommandHandler: IRequestHandler<AddMessageCommand, BaseResponse<Message>>
        {
            IMessageRepository _messageRepository;
            private readonly IMapper _mapper;

            public AddMessageCommandHandler(IMessageRepository messageRepository, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<Message>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
            {
                var message = _mapper.Map<Domain.Entities.Message>(request);
                message.createdDate = DateTime.Now;
                var newResponse = new BaseResponse<Message>();
                newResponse.body = message;
                newResponse.success = await _messageRepository.AddMessage(message);
                return newResponse;
            }

        }
    }
}
