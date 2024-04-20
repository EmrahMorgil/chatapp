using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Features.User.Queries;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Services;

namespace Server.Application.Features.Message.Commands
{
    public class CreateMessageCommand : IRequest<BaseDataResponse<MessageDto>>
    {
        public string Content { get; set; } = null!;
        public string Room { get; set; } = null!;

        public class AddMessageCommandHandler : IRequestHandler<CreateMessageCommand, BaseDataResponse<MessageDto>>
        {
            IMessageRepository _messageRepository;
            IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public AddMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _messageRepository = messageRepository;
                _userRepository = userRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<MessageDto>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                var authUser = JwtService.DecodeToken(authorizationHeader);
                var senderId = authUser.Claims.First().Value;
                if (senderId != null && request.Room.Contains(senderId))
                {
                    var message = _mapper.Map<Domain.Entities.Message>(request);
                    message.SenderId = Guid.Parse(senderId);
                    var messageDto = _mapper.Map<MessageDto>(message);
                    var user = await _userRepository.GetById(Guid.Parse(senderId));
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
}
