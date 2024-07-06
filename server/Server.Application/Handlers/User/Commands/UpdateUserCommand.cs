using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Shared.Consts;
using Server.Shared.Interfaces;
using Server.Shared.Password;
using Server.Shared.Services;
using Server.Shared.Variables;
using Server.Shared.Wrappers;

namespace Server.Application.Handlers.User.Commands;

public class UpdateUserCommand : IRequest<AuthenticationResponse>
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string NewPasswordVerify { get; set; } = null!;
    public string Image { get; set; } = null!;

    public class CreateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthenticationResponse>
    {
        IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var imageName = ImageUploader.UploadImage(request.Image);
            var user = await _userRepository.GetById(Global.UserId);
            var oldPasswordControl = Encryption.VerifyPassword(request.OldPassword, user.Password);
            var existUser = _userRepository.GetByEmail(request.Email);

            if (existUser == null)
            {
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
                        if (request.NewPassword != request.NewPasswordVerify)
                            return new AuthenticationResponse(null!, false, ResponseMessages.NewPasswordsDoNotMatch);
                        if (request.NewPassword == request.OldPassword)
                            return new AuthenticationResponse(null!, false, ResponseMessages.NewPasswordCannotBeTheSameAsOldPassword);
                    }
                    var updateUser = new Domain.Entities.User();
                    updateUser.Id = Global.UserId;
                    updateUser.Email = request.Email;
                    updateUser.Name = request.Name;
                    updateUser.Password = Encryption.EncryptPassword(password);
                    updateUser.Image = imageName != null ? imageName : user.Image;
                    return new AuthenticationResponse(JwtService.CreateToken(updateUser), await _userRepository.Update(updateUser), ResponseMessages.Success);
                }
                else
                {
                    return new AuthenticationResponse(null!, false, ResponseMessages.IncorrectOldPasswordEntry);
                }
            }
            else
            {
                return new AuthenticationResponse(null!, false, ResponseMessages.ThisEmailIsBeingUsed);
            }
        }
    }
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(entity => entity.Email).NotEmpty().NotNull().EmailAddress();
        RuleFor(entity => entity.Name).NotEmpty().NotNull();
        RuleFor(entity => entity.OldPassword).NotEmpty().NotNull();
        RuleFor(entity => entity.Image).NotEmpty().NotNull();
    }
}
