using Lnu_web.Models.ChatModel;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static readonly ConcurrentDictionary<string, string> UserConnections = new();
    private static List<ChatsListModel> activeChats = new List<ChatsListModel>();

    public override Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["userId"].ToString();
        if (!string.IsNullOrEmpty(userId))
        {
            UserConnections[userId] = Context.ConnectionId;
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var user = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        if (!string.IsNullOrEmpty(user.Key))
        {
            UserConnections.TryRemove(user.Key, out _);
        }
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendPrivateMessage(ChatModel message)
    {
        if (UserConnections.TryGetValue(message.ReceiverId, out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }

    public async Task NewChatUpdate(ChatsListModel chat)
    {
        var existingChat = activeChats.FirstOrDefault(c =>
            (c.SenderId == chat.SenderId && c.RecieverId == chat.RecieverId) ||
            (c.SenderId == chat.RecieverId && c.RecieverId == chat.SenderId));

        if (existingChat != null)
        {
            existingChat.Text = chat.Text;
            existingChat.Time = chat.Time;
        }
        else
        {
            var newChatForReceiver = new ChatsListModel
            {
                SenderId = chat.SenderId,
                RecieverId = chat.RecieverId,
                Text = chat.Text,
                Time = chat.Time,
                RecieverName = chat.RecieverName,
                UserPhoto = chat.UserPhoto
            };

            activeChats.Add(newChatForReceiver);

            var newChatForSender = new ChatsListModel
            {
                SenderId = chat.RecieverId,
                RecieverId = chat.SenderId,
                Text = chat.Text,
                Time = chat.Time,
                RecieverName = chat.SenderName,
                UserPhoto = chat.UserPhoto
            };

            activeChats.Add(newChatForSender);
        }

        await Clients.Client(Context.ConnectionId).SendAsync("NewChat", chat);

        if (UserConnections.TryGetValue(chat.RecieverId, out var recieverConnectionId))
        {
            await Clients.Client(recieverConnectionId).SendAsync("NewChat", chat);
        }

        if (UserConnections.TryGetValue(chat.SenderId, out var senderConnectionId))
        {
            await Clients.Client(senderConnectionId).SendAsync("NewChat", chat);
        }
    }

    public async Task MarkMessage(string messageId)
    {
        await Clients.All.SendAsync("MarkAsRead", messageId);
    }

    public async Task JoinGroup(Guid groupId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
    }

    public async Task SendGroupMessage(ChatModel message)
    {
        await Clients.Group(message.ReceiverId.ToString()).SendAsync("ReceiveGroupMessage", message);
    }

}
