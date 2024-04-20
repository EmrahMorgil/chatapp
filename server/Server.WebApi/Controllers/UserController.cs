using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Consts;
using Server.Application.Dto;
using Server.Application.Features.User.Commands;
using Server.Application.Features.User.Queries;
using Server.Application.Handlers.User.Commands;
using Server.Application.Wrappers;

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

        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateUserCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserCommand user)
        {
            return Ok(await _mediator.Send(user));
        }

        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult> List(ListUsersQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserQuery user)
        {
            return Ok(await _mediator.Send(user));
        }
        [HttpPost("detail")]
        [Authorize]
        public async Task<ActionResult> Detail(DetailUserQuery user)
        {
            return Ok(await _mediator.Send(user));
        }
    }
}
