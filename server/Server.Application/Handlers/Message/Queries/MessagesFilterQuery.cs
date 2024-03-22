﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Features.Message.Commands;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;

namespace Server.Application.Features.Message.Queries
{
    public class MessagesFilterQuery : IRequest<BaseDataResponse<List<MessageDto>>>
    {
        public string Room { get; set; } = null!;

        public class MessagesFilterQueryHandler : IRequestHandler<MessagesFilterQuery, BaseDataResponse<List<MessageDto>>>
        {
            IMessageRepository _messageRepository;
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public MessagesFilterQueryHandler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _messageRepository = messageRepository;
                _mapper = mapper;
            }

            public async Task<BaseDataResponse<List<MessageDto>>> Handle(MessagesFilterQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.List();
                var messages = await _messageRepository.List();
                var filteredMessages = messages
                .Where(m => m.Room == request.Room)
                .Join(users,
                    message => message.SenderId,
                    user => user.Id,
                    (message, user) => new MessageDto
                    {
                        Id = message.Id,
                        CreatedDate = message.CreatedDate,
                        SenderUser = _mapper.Map<UserDto>(user),
                        Content = message.Content,
                        Room = message.Room
                    })
                .ToList();

                return new BaseDataResponse<List<MessageDto>>(filteredMessages, true, ResponseMessages.Success);
            }

        }
    }
    public class MessagesFilterQueryValidator : AbstractValidator<MessagesFilterQuery>
    {
        public MessagesFilterQueryValidator()
        {
            RuleFor(entity => entity.Room).NotEmpty().NotNull();
        }
    }
}
