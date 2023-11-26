using AutoMapper;
using MediatR;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<BaseResponse<User>>
    {
        public Guid id { get; set; }
        public string? email { get; set; }
        public string? name { get; set; }
        public string? password { get; set; }
        public string? image { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<User>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateUserCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<BaseResponse<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                request.password = Encryption.EncryptPassword(request.password);
                return await _userRepository.UpdateUser(request);
            }

        }

    }
}
