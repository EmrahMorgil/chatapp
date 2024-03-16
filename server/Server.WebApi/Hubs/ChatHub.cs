using Microsoft.AspNetCore.SignalR;

namespace Server.WebApi.Hubs
{
    public class OnlineUsers
    {
        public List<string> UsersIds { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string LastUserId { get; set; } = null!;
    }
    public class ChatHub : Hub
    {
        static List<string> activeUserIds = new List<string>();

        public override async Task OnConnectedAsync()
        {
            var onlineUsers = new OnlineUsers();
            var id = Context.GetHttpContext()?.Request.Query["userid"];
            var username = Context.GetHttpContext()?.Request.Query["username"];
            var message = $"{username} has joined";

            activeUserIds.Add(id);
            onlineUsers.Message = message;
            onlineUsers.UsersIds = activeUserIds;
            onlineUsers.LastUserId = id;

            await Clients.All.SendAsync("UserConnection", onlineUsers);
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var onlineUsers = new OnlineUsers();
            var id = Context.GetHttpContext()?.Request.Query["userid"];
            var username = Context.GetHttpContext()?.Request.Query["username"];
            var message = $"{username} has disconnect from server";
            activeUserIds.Remove(id);
            onlineUsers.Message = message;
            onlineUsers.UsersIds = activeUserIds;

            await Clients.Others.SendAsync("UserConnection", onlineUsers);
        }
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
