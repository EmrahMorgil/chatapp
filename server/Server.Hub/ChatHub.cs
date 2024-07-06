using Microsoft.AspNetCore.SignalR;
using Server.Domain.Entities;
using Server.Shared.Dtos;
using Server.Shared.Interfaces;

namespace Server.Realtime;

public class Notification
{
    public List<UserDto> OnlineUsers { get; set; } = null!;
    public UserDto User { get; set; } = null!;
    public string Status { get; set; } = null!;
}
public class ChatHub : Hub
{
    static List<UserDto> OnlineUsers = new List<UserDto>();
    IUserRepository _userRepository;

    public ChatHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public override async Task OnConnectedAsync()
    {
        var id = Context.GetHttpContext()?.Request.Query["userid"];
        var user = await _userRepository.GetById(Guid.Parse(id!));
        var userDto = new UserDto() { Id = user.Id, Name =  user.Name, Image = user.Image };
        OnlineUsers.Add(userDto);
        var notification = new Notification();
        notification.User = userDto;
        notification.Status = "join";
        notification.OnlineUsers = OnlineUsers;

        await Clients.All.SendAsync("UserConnection", notification);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var id = Context.GetHttpContext()?.Request.Query["userid"];
        var user = await _userRepository.GetById(Guid.Parse(id!));
        var userDto = new UserDto() { Id = user.Id, Name = user.Name, Image = user.Image };
        var findUser = OnlineUsers.FirstOrDefault(x => x.Id == user.Id);
        OnlineUsers.Remove(findUser!);
        var notification = new Notification();
        notification.User = userDto;
        notification.Status = "leave";
        notification.OnlineUsers = OnlineUsers;

        await Clients.Others.SendAsync("UserConnection", notification);
    }
    public async Task JoinRoom(string roomName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
    }
}
