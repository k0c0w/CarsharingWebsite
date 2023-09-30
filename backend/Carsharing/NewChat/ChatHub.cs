using Microsoft.AspNetCore.SignalR;

namespace Carsharing.NewChat
{
    public class ChatHub : Hub
    {

        // send notification to all users
        public async Task SendMessageAsync(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }

        public async Task JoinChat(string user, string message)
        {
            await Clients.Others.SendAsync("RecieveMesssage", user, message);
        }
    }
}
