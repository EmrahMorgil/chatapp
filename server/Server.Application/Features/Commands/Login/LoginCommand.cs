using AutoMapper;
using MediatR;
using Server.Application.Interfaces.Repository;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.Login
{
    public class LoginCommand : IRequest<User>
    {
        public string? email { get; set; }
        public string? password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, User>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<User> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<Domain.Entities.User>(request);
                var loginUser = await _userRepository.LoginUser(user);
                return loginUser;
            }
        }

    }
}
