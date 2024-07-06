using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces;
using Server.Application.Variables;
using Server.Application.Wrappers;
using Server.Persistence.Services;

namespace Server.Application.Features.Message.Commands;

public class CreateMessageCommand : IRequest<BaseDataResponse<MessageDto>>
{
    public string Content { get; set; } = null!;
    public string Room { get; set; } = null!;

    public class AddMessageCommandHandler : IRequestHandler<CreateMessageCommand, BaseDataResponse<MessageDto>>
    {
        IMessageRepository _messageRepository;
        IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseDataResponse<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            if (request.Room.Contains(Global.UserId.ToString()))
            {
                var message = _mapper.Map<Domain.Entities.Message>(request);
                message.SenderId = Global.UserId;
                var messageDto = _mapper.Map<MessageDto>(message);
                var user = await _userRepository.GetById(Global.UserId);
                messageDto.SenderUser = _mapper.Map<UserDto>(user);
                return new BaseDataResponse<MessageDto>(messageDto, await _messageRepository.Create(message), ResponseMessages.Success);
            }
            else
            {
                return new BaseDataResponse<MessageDto>(null!, false, ResponseMessages.UnauthorizedEntry);

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
