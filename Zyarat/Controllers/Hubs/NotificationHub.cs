using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Enumerable = System.Linq.Enumerable;

namespace Zyarat.Controllers.Hubs
{
    [Authorize]
    public class NotificationHub:Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            var id = Context.User.FindFirst(claim => claim.Type == "id")?.Value;
            if (!ClientsRepo.Users.Contains(id))
            {
                lock (ClientsRepo.Users)
                {
                    ClientsRepo.Users.Add(id);
                }
            }
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.User.FindFirst(claim => claim.Type == "id")?.Value;
            if (ClientsRepo.Users.Contains(id))
            {
                lock (ClientsRepo.Users)
                {
                    ClientsRepo.Users.Remove(id);
                }
            }
            return base.OnDisconnectedAsync(exception);
        }

        //hub
        public async  Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,group);
        }
        //hub
        public async Task SendVisit(string group,int visitId)
        {
            await Clients.Group(group).ReceiveVisit(visitId);
        }
        //hub
        public async Task SendEvaluation(string group,int evaluationIdId)
        { 
            await Clients.Group(group).ReceiveEvaluation(evaluationIdId);
        }
        //hub
        public async Task SendNotification( int notificationId,string  userId)
        {
            await Clients.User(userId).ReceiveNotification( notificationId);
        }
        //hub
        public async Task SendMessageToAll(string messageType, int messageId)
        {
            await Clients.All.ReceiveMessage(messageType, messageId);
        }
        //hub
        public async Task SendMessageToUsers(List<string>usersIds,string messageType, int messageId)
        {
            await Clients.Users(usersIds).ReceiveMessage(messageType, messageId);
        }

    }
}