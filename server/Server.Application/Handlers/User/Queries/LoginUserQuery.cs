using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Interfaces;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Persistence.Services;

namespace Server.Application.Features.User.Queries;

public class LoginUserQuery : IRequest<AuthenticationResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, AuthenticationResponse>
    {
        IUserRepository _userRepository;

        public LoginUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if(user != null)
            {
                if(Encryption.VerifyPassword(request.Password, user.Password))
                {
                    return new AuthenticationResponse(JwtService.CreateToken(user), true, ResponseMessages.Success);
                }
                else
                {
                    return new AuthenticationResponse(null!, false, ResponseMessages.InvalidCredentials);
                }
            }
            else
            {
                return new AuthenticationResponse(null!, false, ResponseMessages.InvalidCredentials);
            }
        }
    }
}
public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(entity => entity.Email).NotEmpty().NotNull();
        RuleFor(entity => entity.Password).NotEmpty().NotNull();
    }
}
