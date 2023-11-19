using Microsoft.AspNetCore.SignalR;

namespace Server.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        static List<string> clients = new List<string>();
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        //Hub'a bir client bağlandığında tetiklenir.
        public override async Task OnConnectedAsync()
        {
            clients.Add(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        }

        //Hub'dan bir client çıktığında tetiklenir.
        //OnDisconnectAsync
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clients.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("clients", clients);
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has disconnect from server");
        }
    }
}
