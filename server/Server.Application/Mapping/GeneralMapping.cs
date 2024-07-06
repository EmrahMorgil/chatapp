using AutoMapper;
using Server.Application.Dto;
using Server.Application.Dtos;
using Server.Application.Features.Message.Commands;
using Server.Application.Features.User.Commands;

namespace Server.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<CreateMessageCommand, Domain.Entities.Message>();

        CreateMap<MessageDto, Domain.Entities.Message>();

        CreateMap<CreateUserCommand, Domain.Entities.User>();

        CreateMap<UserDto, Domain.Entities.User>();

        CreateMap<UserDetailDto, Domain.Entities.User>();
    }
}
