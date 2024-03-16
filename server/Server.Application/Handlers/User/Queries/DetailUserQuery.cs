using AutoMapper;
using FluentValidation;
using MediatR;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Handlers.User.Commands;
using Server.Application.Interfaces.Repository;
using Server.Application.Password;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using Server.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Features.User.Queries
{
    public class DetailUserQuery : IRequest<BaseDataResponse<UserDto>>
    {
        public Guid Id { get; set; }

        public class DetailUserQueryHandler : IRequestHandler<DetailUserQuery, BaseDataResponse<UserDto>>
        {
            IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public DetailUserQueryHandler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<BaseDataResponse<UserDto>> Handle(DetailUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetById(request.Id);
                var userDto = _mapper.Map<UserDto>(user);
                return new BaseDataResponse<UserDto>(userDto, true, ResponseMessages.Success);
            }
        }
    }
    public class DetailUserQueryValidator : AbstractValidator<DetailUserQuery>
    {
        public DetailUserQueryValidator()
        {
            RuleFor(entity => entity.Id).NotEmpty().NotNull();
        }
    }
}
