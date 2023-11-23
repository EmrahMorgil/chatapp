using AutoMapper;
using MediatR;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string? email { get; set; }
        public string? password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public LoginCommandHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                return await _userRepository.LoginUser(request);
            }
        }

    }
}
