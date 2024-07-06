using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Server.Application.Consts;
using Server.Application.Dtos;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;
using Server.Persistence.Services;

namespace Server.Application.Features.User.Queries;

public class DetailUserQuery : IRequest<BaseDataResponse<UserDetailDto>>
{
    public class DetailUserQueryHandler : IRequestHandler<DetailUserQuery, BaseDataResponse<UserDetailDto>>
    {
        IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailUserQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseDataResponse<UserDetailDto>> Handle(DetailUserQuery request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var authUser = JwtService.DecodeToken(authorizationHeader);
            var authId = authUser.Claims.First().Value;

            if (authId != null)
            {
                var user = await _userRepository.GetById(Guid.Parse(authId));
                var userDto = _mapper.Map<UserDetailDto>(user);
                return new BaseDataResponse<UserDetailDto>(userDto, true, ResponseMessages.Success);
            }
            else
            {
                return new BaseDataResponse<UserDetailDto>(null!, false, ResponseMessages.UnauthorizedEntry);
            }
        }
    }
}
