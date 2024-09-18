using Microsoft.AspNetCore.SignalR;

namespace AccountService.API.Hubs;

public class AnimeHub : Hub
{
    public async Task SendPlaySignal(string groupName, string videoUrl)
    {
        await Clients.Group(groupName).SendAsync("ReceivePlaySignal", videoUrl);
    }

    public override async Task OnConnectedAsync()
    {
        // При подключении клиента добавляем его в группу
        await Groups.AddToGroupAsync(Context.ConnectionId, "videoGroup");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // При отключении клиента удаляем его из группы
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "videoGroup");
        await base.OnDisconnectedAsync(exception);
    }
}