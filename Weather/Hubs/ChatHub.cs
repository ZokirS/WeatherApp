using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Weather.Hubs
{
    
    public class ChatHub: Hub
    {
        public async Task SendMessage( string message)
        {
            await Clients.All.SendAsync("newMessage", "anonymous", message);
        }
    }
}
