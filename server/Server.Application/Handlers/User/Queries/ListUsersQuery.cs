using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Interfaces.Repository;
using Server.Application.Wrappers;


namespace Server.Application.Features.User.Queries
{
    public class ListUsersQuery : IRequest<BaseDataResponse<List<UserDto>>>
    {
        public Guid Id { get; set; }
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
                var userList = await _userRepository.List();
                var filteredUserList = userList.Where((u)=>u.Id != request.Id);
                var mapperUserList = filteredUserList.Select(u => _mapper.Map<UserDto>(u)).ToList();

                return new BaseDataResponse<List<UserDto>>(mapperUserList, true, ResponseMessages.Success);
            }
        }
    }
    public class ListUsersQueryValidator : AbstractValidator<ListUsersQuery>
    {
        public ListUsersQueryValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
