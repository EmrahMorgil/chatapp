using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Dto;
using Server.Application.Features.Message.Commands;
using Server.Application.Features.Message.Queries;
using Server.Application.Wrappers;
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

        [HttpPost("list")]
        [Authorize]
        public async Task<ActionResult<BaseDataResponse<List<MessageDto>>>> List(MessagesFilterQuery command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<BaseDataResponse<Guid>>> Create(CreateMessageCommand command)
        {
            var response = await _mediator.Send(command);
            await _hubContext.Clients.Group(command.Room).SendAsync("ReceiveMessage", response.Body);
            return Ok(response);
        }

    }
}
