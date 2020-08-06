using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Zyarat.Controllers.Hubs
{
    
    public class NotificationHub:Hub
    {
        public Task SendNotificationToAUser(string userId,string message)
        {
            return Clients.User(userId).SendAsync("displayMessage", message);
        }

       
    }
}