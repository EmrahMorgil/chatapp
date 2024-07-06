using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces;
using Server.Application.Variables;
using Server.Application.Wrappers;
using Server.Realtime;

namespace Server.Application.Features.Message.Commands;

public class CreateMessageCommand : IRequest<BaseResponse>
{
    public string Content { get; set; } = null!;
    public string Room { get; set; } = null!;

    public class AddMessageCommandHandler : IRequestHandler<CreateMessageCommand, BaseResponse>
    {
        IMessageRepository _messageRepository;
        IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;


        public AddMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public async Task<BaseResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if (request.Room.Contains(Global.UserId.ToString()))
            {
                var message = _mapper.Map<Domain.Entities.Message>(request);
                message.UserId = Global.UserId;
                var messageDto = _mapper.Map<MessageDto>(message);
                //SignalR
                await _hubContext.Clients.Group(message.Room).SendAsync("ReceiveMessage", message.Content);
                return new BaseResponse(await _messageRepository.Create(message), ResponseMessages.Success);
            }
            else
            {
                return new BaseResponse(false, ResponseMessages.UnauthorizedEntry);

            }

        }
    }
}
public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageCommandValidator()
    {
        RuleFor(entity => entity.Content).NotEmpty().NotNull();
        RuleFor(entity => entity.Room).NotEmpty().NotNull();
    }
}
