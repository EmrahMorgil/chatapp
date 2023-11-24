using Microsoft.AspNetCore.SignalR;



namespace Server.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var id = Context.GetHttpContext().Request.Query["id"];
            var username = Context.GetHttpContext().Request.Query["username"];

            await Clients.Others.SendAsync("UserConnection", $"{username} has joined");
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.GetHttpContext().Request.Query["id"];
            var username = Context.GetHttpContext().Request.Query["username"];

            await Clients.Others.SendAsync("UserConnection", $"{username} has disconnect from server");
        }
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
