using System.Threading.Tasks;

namespace Zyarat.Controllers.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveNotification(int notificationId);
        Task ReceiveMessage(string type,int messageId);
        Task ReceiveVisit(int visitId);
        Task ReceiveEvaluation(int evaluationId);
    }
}