using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Features.Commands.AddMessage;
using Server.Application.Features.Commands.GetAllMessages;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetMessages")]
        public async Task<IActionResult> Get(GetAllMessagesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("AddMessage")]
        public async Task<IActionResult> Post(AddMessageCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
