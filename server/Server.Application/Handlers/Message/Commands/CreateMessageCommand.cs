using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Features.User.Queries;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;

namespace Server.Application.Features.Message.Commands
{
    public class CreateMessageCommand : IRequest<BaseDataResponse<MessageDto>>
    {
        public Guid SenderId { get; set; }
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
                var message = _mapper.Map<Domain.Entities.Message>(request);
                var messageDto = _mapper.Map<MessageDto>(message);
                var user = await _userRepository.GetById(request.SenderId);
                messageDto.SenderUser = _mapper.Map<UserDto>(user);

                return new BaseDataResponse<MessageDto>(messageDto, await _messageRepository.Create(message), ResponseMessages.Success);
            }
        }
    }
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(entity => entity.SenderId).NotEmpty().NotNull();
            RuleFor(entity => entity.Content).NotEmpty().NotNull();
            RuleFor(entity => entity.Room).NotEmpty().NotNull();
        }
    }
}
