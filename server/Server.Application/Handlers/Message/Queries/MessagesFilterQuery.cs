using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces;
using Server.Application.Variables;
using Server.Application.Wrappers;
using Server.Persistence.Services;
using Server.Realtime;

namespace Server.Application.Features.Message.Queries;

public class MessagesFilterQuery : IRequest<BaseDataResponse<List<MessageDto>>>
{
    public string Room { get; set; } = null!;

    public class MessagesFilterQueryHandler : IRequestHandler<MessagesFilterQuery, BaseDataResponse<List<MessageDto>>>
    {
        IMessageRepository _messageRepository;
        IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesFilterQueryHandler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<BaseDataResponse<List<MessageDto>>> Handle(MessagesFilterQuery request, CancellationToken cancellationToken)
        {
            if(request.Room.Contains(Global.UserId.ToString()))
            {
                var messages = await _messageRepository.GetFilteredMessages(request.Room);
                return new BaseDataResponse<List<MessageDto>>(messages, true, ResponseMessages.Success);
            }
            else
            {
                return new BaseDataResponse<List<MessageDto>>(null!, false, ResponseMessages.UnauthorizedEntry);
            }
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
