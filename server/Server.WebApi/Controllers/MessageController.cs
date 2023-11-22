using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Features.Commands.AddMessage;
using Server.Application.Features.Commands.GetAllMessages;
using Server.Domain.Entities;
using Server.WebApi.Hubs;

namespace Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        readonly IHubContext<ChatHub> _hubContext;

        private readonly IMediator _mediator;

        public MessageController(IMediator mediator, IHubContext<ChatHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost("GetMessages")]
        public async Task<IActionResult> Get(GetAllMessagesCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("AddMessage")]
        public async Task<IActionResult> Post(AddMessageCommand command)
        {
            var request = await _mediator.Send(command);
            await _hubContext.Clients.Group(command.room).SendAsync("ReceiveMessage", request.body);
            return Ok(request);
        }

    }
}
