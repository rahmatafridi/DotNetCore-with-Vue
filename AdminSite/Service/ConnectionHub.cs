using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AdminSite.Service
{
    public class ConnectionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            //await Clients.All.SendAsync("UserConnected", Context.ConnectionId, dd);
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
