using Microsoft.AspNetCore.SignalR;
using Server.Application.Dto;

namespace Server.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        static List<string> activeUsers = new List<string>();

        public override async Task OnConnectedAsync()
        {
            var UserConnect = new UserConnect();
            var id = Context.GetHttpContext().Request.Query["userid"];
            var username = Context.GetHttpContext().Request.Query["username"];
            var message = $"{username} has joined";
            
            activeUsers.Add(id);
            UserConnect.message = message;
            UserConnect.usersIds = activeUsers;
            UserConnect.lastUserId = id;

            await Clients.All.SendAsync("UserConnection", UserConnect);
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var UserConnect = new UserConnect();
            var id = Context.GetHttpContext().Request.Query["userid"];
            var username = Context.GetHttpContext().Request.Query["username"];
            var message = $"{username} has disconnect from server";
            activeUsers.Remove(id);
            UserConnect.message = message;
            UserConnect.usersIds = activeUsers;

            await Clients.Others.SendAsync("UserConnection", UserConnect);
        }
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
