
using Microsoft.AspNetCore.SignalR;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebApp.Hubs
{
    public class SupportHub : Hub
    {
        public Task SendMessage(string user,string message)
        {
            return Clients.Others.SendAsync("ReceiveMessage", user,message);
        }

        public void Register(long userId)
        {
            Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString(CultureInfo.InvariantCulture));

        }

        public Task ToGroup(dynamic id, string message)
        {
            return Clients.Group(id.ToString()).SendMessage(message);
        }
    }

}
