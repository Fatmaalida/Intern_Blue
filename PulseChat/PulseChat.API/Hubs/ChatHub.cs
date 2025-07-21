using Microsoft.AspNetCore.SignalR;

namespace PulseChat.API.Hubs;

public class ChatHub : Hub
{
    // 1. Mesaj gönderme
    public async Task SendMessage(string groupId, string user, string message)
    {
        await Clients.Group(groupId).SendAsync("ReceiveMessage", user, message);
    }

    // 2. Gruba katılma
    public async Task JoinGroup(string groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("UserJoined", Context.ConnectionId);
    }

    // 3. Gruptan çıkma
    public async Task LeaveGroup(string groupId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        await Clients.Group(groupId).SendAsync("UserLeft", Context.ConnectionId);
    }
}
