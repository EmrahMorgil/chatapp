using Microsoft.AspNetCore.SignalR;
using Server.WebApi.Hubs;

namespace Server.WebApi.Business
{
    public class MyBusiness
    {
        readonly IHubContext<ChatHub> _hubContext;

        public MyBusiness(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
