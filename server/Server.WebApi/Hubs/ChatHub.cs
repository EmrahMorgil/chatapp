using Microsoft.AspNetCore.SignalR;

namespace Server.WebApi.Hubs
{
    public class OnlineUsers
    {
        public List<string> UsersIds { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string LastUserId { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
    public class ChatHub : Hub
    {
        static List<string> activeUserIds = new List<string>();

        public override async Task OnConnectedAsync()
        {
            var onlineUsers = new OnlineUsers();
            var id = Context.GetHttpContext()?.Request.Query["userid"];
            var userName = Context.GetHttpContext()?.Request.Query["username"];
            var image = Context.GetHttpContext()?.Request.Query["image"];

            activeUserIds.Add(id);
            onlineUsers.UserName = userName;
            onlineUsers.UsersIds = activeUserIds;
            onlineUsers.Image = image;
            onlineUsers.LastUserId = id;
            onlineUsers.Status = "connect";

            await Clients.All.SendAsync("UserConnection", onlineUsers);
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var onlineUsers = new OnlineUsers();
            var id = Context.GetHttpContext()?.Request.Query["userid"];
            var username = Context.GetHttpContext()?.Request.Query["username"];
            var image = Context.GetHttpContext()?.Request.Query["image"];

            activeUserIds.Remove(id);
            onlineUsers.UserName = username;
            onlineUsers.UsersIds = activeUserIds;
            onlineUsers.Image = image;
            onlineUsers.Status = "disconnect";

            await Clients.Others.SendAsync("UserConnection", onlineUsers);
        }
        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
    }
}
