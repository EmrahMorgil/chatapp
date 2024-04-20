using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Persistence.Services;

namespace Server.Application.Features.User.Queries
{
    public class ListUsersQuery : IRequest<BaseDataResponse<List<UserDto>>>
    {
        public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, BaseDataResponse<List<UserDto>>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ListUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<BaseDataResponse<List<UserDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                var authUser = JwtService.DecodeToken(authorizationHeader);
                var authId = authUser.Claims.First().Value;

                if (authId != null)
                {
                    var userList = await _userRepository.List();
                    var filteredUserList = userList.Where((u) => u.Id != Guid.Parse(authId));
                    var mapperUserList = filteredUserList.Select(u => _mapper.Map<UserDto>(u)).ToList();
                    return new BaseDataResponse<List<UserDto>>(mapperUserList, true, ResponseMessages.Success);
                }
                else
                {
                    return new BaseDataResponse<List<UserDto>>(null!, false, ResponseMessages.UnauthorizedEntry);
                }
            }
        }
    }
}
