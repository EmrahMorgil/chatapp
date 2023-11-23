using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Commands.CreateUser;
using Server.Application.Features.Commands.GetAllMessages;
using Server.Application.Features.Commands.GetUsers;
using Server.Application.Features.Commands.Login;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post(CreateUserCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("GetUsers")]
        [Authorize]
        public async Task<IActionResult> Get(GetUsersCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
