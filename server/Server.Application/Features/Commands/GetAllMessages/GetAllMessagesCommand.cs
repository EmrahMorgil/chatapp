﻿using AutoMapper;
using MediatR;
using Server.Application.Features.Commands.CreateUser;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.GetAllMessages
{
    public class GetAllMessagesCommand: IRequest<BaseResponse<MessagesResponse>>
    {
        public Guid? takerId { get; set; }
        public Guid? senderId { get; set; }


        public class GetAllMessagesHandler : IRequestHandler<GetAllMessagesCommand, BaseResponse<MessagesResponse>>
        {
            IMessageRepository _messageRepository;
            private readonly IMapper _mapper;

            public GetAllMessagesHandler(IMessageRepository messageRepository, IMapper mapper)
            {
                _messageRepository = messageRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<MessagesResponse>> Handle(GetAllMessagesCommand request, CancellationToken cancellationToken)
            {

                var newResponse = new BaseResponse<MessagesResponse>();
                newResponse.body = await _messageRepository.GetAllMessages(request);
                if (newResponse.body != null)
                    newResponse.success = true;
                else
                    newResponse.success = false;
                return newResponse;
            }

        }
    }
}
