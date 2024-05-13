using Microsoft.AspNetCore.SignalR;

namespace StackOverflowClone.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification()
        {
            await Clients.All.SendAsync("ReceiveNotification", "Beware of malicious links.\n Do not click unknown links!");
        }
    }
}
