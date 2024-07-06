using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server.Application.Features.Message.Commands;
using Server.Application.Features.Message.Queries;
using Server.WebApi.Hubs;

namespace Server.WebApi.Controllers;

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
    public async Task<ActionResult> List(MessagesFilterQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<ActionResult> Create(CreateMessageCommand command)
    {
        var response = await _mediator.Send(command);
        await _hubContext.Clients.Group(command.Room).SendAsync("ReceiveMessage", response.Body);
        return Ok(response);
    }

}
