using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Interfaces;
using Server.Application.Password;
using Server.Application.Services;
using Server.Application.Wrappers;
using Server.Persistence.Services;

namespace Server.Application.Features.User.Commands;

public class CreateUserCommand : IRequest<AuthenticationResponse>
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Image { get; set; } = null!;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthenticationResponse>
    {
        IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var imageName = ImageUploader.UploadImage(request.Image);
            if (imageName != null)
            {
                var existUser = _userRepository.GetByEmail(request.Email);
                if (existUser == null)
                {
                    var user = _mapper.Map<Domain.Entities.User>(request);
                    user.Image = imageName;
                    user.Password = Encryption.EncryptPassword(request.Password);
                    return new AuthenticationResponse(JwtService.CreateToken(user), await _userRepository.Create(user), ResponseMessages.Success);
                }
                else
                {
                    return new AuthenticationResponse(null!, false, ResponseMessages.UserAlreadyExist);
                }
            }
            else
            {
                return new AuthenticationResponse(null!, false, ResponseMessages.AnErrorOccurredWhileLoadingTheImage);
            }
            
        }
    }
}
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(entity => entity.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(entity => entity.Name).NotEmpty().NotNull();
        RuleFor(entity => entity.Password).NotEmpty().NotNull();
        RuleFor(entity => entity.Image).NotEmpty().NotNull();
    }
}
