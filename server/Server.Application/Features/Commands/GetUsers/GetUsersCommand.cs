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

namespace Server.Application.Features.Commands.GetUsers
{
    public class GetUsersCommand : IRequest<BaseResponse<List<User>>>
    {
        public Guid? id { get; set; }
        public string? token { get; set; }


        public class GetUsersHandler : IRequestHandler<GetUsersCommand, BaseResponse<List<User>>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public GetUsersHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseResponse<List<User>>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
            {

                var newResponse = new BaseResponse<List<User>>();
                newResponse.body = await _userRepository.GetUsers(request);
                if (newResponse.body != null)
                    newResponse.success = true;
                else
                    newResponse.success = false;
                return newResponse;
            }

        }
    }
}
