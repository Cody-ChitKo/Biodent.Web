using Biodent.Models;
using Microsoft.AspNetCore.SignalR;
using MySqlX.XDevAPI;

namespace Biodent.Web.Hubs
{
	public class ChatHub : Hub
	{
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
