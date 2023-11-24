using AutoMapper;
using MediatR;
using Server.Application.Features.Commands.AddMessage;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.CreateUser
{
    public class CreateUserCommand: IRequest<AuthenticationResponse>
    {
        public string? email { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? image { get; set; }

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
                var user = _mapper.Map<Domain.Entities.User>(request);
                user.id = Guid.NewGuid();
                user.createdDate = DateTime.Now;
                user.password = Encryption.EncryptPassword(request.password);
                return await _userRepository.CreateUser(user);
            }

        }

    }
}
