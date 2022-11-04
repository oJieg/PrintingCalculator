using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace printing_calculator.controllers.SignalRApp
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.Others.SendAsync("Send", message);
        }
    }
}