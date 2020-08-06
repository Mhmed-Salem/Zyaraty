using System.Linq;
using Microsoft.AspNetCore.SignalR;

namespace Zyarat.Controllers.Hubs
{
    public class MyUserProvider:IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
        }
    }
}