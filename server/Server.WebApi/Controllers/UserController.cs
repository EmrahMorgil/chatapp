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
        public async Task<ActionResult> Create(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult> Update(UpdateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> List([FromQuery]ListUsersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        
        [HttpGet("detail")]
        [Authorize]
        public async Task<ActionResult> Detail([FromQuery]DetailUserQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
