using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Factories;

namespace Zyarat.Models.Repositories.NotificationRepo
{
    public interface INotificationTypeRepo
    {
         Task<NotificationType> Get(NotificationTypesEnum type);
    }
}