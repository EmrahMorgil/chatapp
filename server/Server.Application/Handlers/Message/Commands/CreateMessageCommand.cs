using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Server.Realtime;
using Server.Shared.Consts;
using Server.Shared.Dtos;
using Server.Shared.Interfaces;
using Server.Shared.Variables;
using Server.Shared.Wrappers;

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
                var user = await _userRepository.GetById(Global.UserId);
                var messageDto = _mapper.Map<MessageDto>(message);
                messageDto.UserImage = user.Image;
                messageDto.UserName = user.Name;
                //SignalR
                await _hubContext.Clients.Group(message.Room).SendAsync("ReceiveMessage", messageDto);
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
