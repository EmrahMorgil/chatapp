using AutoMapper;
using Server.Application.Features.Commands.AddMessage;
using Server.Application.Features.Commands.CreateUser;
using Server.Application.Features.Commands.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Domain.Entities.Message, AddMessageCommand>()
                .ReverseMap();

            CreateMap<Domain.Entities.User, CreateUserCommand>()
               .ReverseMap();

            CreateMap<Domain.Entities.User, LoginCommand>()
               .ReverseMap();
        }
    }
}
