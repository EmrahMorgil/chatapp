using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces;
using Server.Application.Variables;
using Server.Application.Wrappers;

namespace Server.Application.Features.User.Queries;

public class ListUsersQuery : IRequest<BaseDataResponse<List<UserDto>>>
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, BaseDataResponse<List<UserDto>>>
    {
        IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<BaseDataResponse<List<UserDto>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var filteredUserList = await _userRepository.ListUsersExcludingId(Global.UserId);
            return new BaseDataResponse<List<UserDto>>(filteredUserList, true, ResponseMessages.Success);
        }
    }
}
