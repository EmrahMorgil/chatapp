using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Features.User.Commands;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Services;

namespace Server.Application.Handlers.User.Commands
{
    public class UpdateUserCommand : IRequest<AuthenticationResponse>
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string NewPasswordVerify { get; set; } = null!;
        public string Image { get; set; } = null!;

        public class CreateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthenticationResponse>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<AuthenticationResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetById(request.Id);
                var oldPasswordControl = Encryption.VerifyPassword(request.OldPassword, user.Password);
                if (oldPasswordControl)
                {
                    string password;

                    if (String.IsNullOrEmpty(request.NewPassword))
                    {
                        password = request.OldPassword;
                    }
                    else
                    {
                        password = request.NewPassword;
                    }

                    if (!String.IsNullOrEmpty(request.NewPassword) && !String.IsNullOrEmpty(request.NewPasswordVerify))
                        {
                        if(request.NewPassword != request.NewPasswordVerify)
                            return new AuthenticationResponse(null!, false, null!, ResponseMessages.NewPasswordsDoNotMatch);
                        if(request.NewPassword == request.OldPassword)
                            return new AuthenticationResponse(null!, false, null!, ResponseMessages.NewPasswordCannotBeTheSameAsOldPassword);
                        }

                    var updateUser = new Domain.Entities.User();
                        updateUser.Id = request.Id;
                        updateUser.Email = request.Email;
                        updateUser.Name = request.Name;
                        updateUser.Password = Encryption.EncryptPassword(password);
                        updateUser.Image = request.Image;
                    return new AuthenticationResponse(updateUser, await _userRepository.Update(updateUser), JwtService.GenerateToken(updateUser.Email), ResponseMessages.Success);
                }
                else
                {
                    return new AuthenticationResponse(null!, false, null!, ResponseMessages.IncorrectOldPasswordEntry);
                }
            }
        }
    }

    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
            RuleFor(entity => entity.Email).NotEmpty().NotNull();
            RuleFor(entity => entity.Name).NotEmpty().NotNull();
            RuleFor(entity => entity.OldPassword).NotEmpty().NotNull();
            RuleFor(entity => entity.Image).NotEmpty().NotNull();
        }
    }
}
