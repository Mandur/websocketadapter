using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace websocketadapter
{
    public class MessagingHub : Hub
    {
       public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
     
    }
}