using AutoMapper;
using MediatR;
using Server.Shared.Consts;
using Server.Shared.Dtos;
using Server.Shared.Interfaces;
using Server.Shared.Variables;
using Server.Shared.Wrappers;

namespace Server.Application.Features.User.Queries;

public class DetailUserQuery : IRequest<BaseDataResponse<UserDetailDto>>
{
    public class DetailUserQueryHandler : IRequestHandler<DetailUserQuery, BaseDataResponse<UserDetailDto>>
    {
        IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DetailUserQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseDataResponse<UserDetailDto>> Handle(DetailUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(Global.UserId);
            var userDto = _mapper.Map<UserDetailDto>(user);
            return new BaseDataResponse<UserDetailDto>(userDto, true, ResponseMessages.Success);
        }
    }
}
