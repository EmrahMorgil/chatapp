using Server.Application.Dto;
using Server.Application.Features.Commands.GetUsers;
using Server.Application.Features.Commands.Login;
using Server.Application.Wrappers;
using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<AuthenticationResponse> CreateUser(User user);
        Task<AuthenticationResponse> LoginUser(LoginCommand user);
        Task<List<UserViewDto>> GetUsers(GetUsersCommand entity);
    }
}
