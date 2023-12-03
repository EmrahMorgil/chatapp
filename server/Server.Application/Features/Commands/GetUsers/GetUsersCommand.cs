using AutoMapper;
using MediatR;
using Server.Application.Dto;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.Commands.GetUsers
{
    public class GetUsersCommand : IRequest<BaseResponse<List<UserViewDto>>>
    {
        public Guid? id { get; set; }
        public string? token { get; set; }


        public class GetUsersHandler : IRequestHandler<GetUsersCommand, BaseResponse<List<UserViewDto>>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<UserViewDto>>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
            {

                return await _userRepository.GetUsers(request);
            }

        }
    }
}
