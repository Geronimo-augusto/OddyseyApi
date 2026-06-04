using Microsoft.AspNetCore.SignalR;

namespace GsApi.Hubs;

public class AlertHub : Hub
{
    public async Task JoinAlertChannel()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "DisasterAlerts");
    }
}