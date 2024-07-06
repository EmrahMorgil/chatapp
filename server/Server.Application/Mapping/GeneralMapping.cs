using AutoMapper;
using Server.Application.Features.Message.Commands;
using Server.Application.Features.User.Commands;
using Server.Shared.Dtos;

namespace Server.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        //Message
        CreateMap<CreateMessageCommand, Domain.Entities.Message>();

        CreateMap<Domain.Entities.Message, MessageDto>();

        //User
        CreateMap<CreateUserCommand, Domain.Entities.User>();

        CreateMap<Domain.Entities.User, UserDto>();

        CreateMap<Domain.Entities.User, UserDetailDto>();
    }
}
