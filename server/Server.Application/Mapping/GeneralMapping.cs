﻿using AutoMapper;
using Server.Application.Dto;
using Server.Application.Dtos;
using Server.Application.Features.Message.Commands;
using Server.Application.Features.User.Commands;

namespace Server.Application.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Domain.Entities.Message, CreateMessageCommand>()
            .ReverseMap();

        CreateMap<Domain.Entities.Message, MessageDto>()
            .ReverseMap();

        CreateMap<Domain.Entities.User, CreateUserCommand>()
           .ReverseMap();

        CreateMap<Domain.Entities.User, UserDto>()
           .ReverseMap();

        CreateMap<Domain.Entities.User, UserDetailDto>()
           .ReverseMap();
    }
}
