using System.Threading.Tasks;
using Zyarat.Data;

namespace Zyarat.Models.Factories.NotificationFactory
{
    public interface INotificationEventFactory
    {
        Task<EventNotification> CreateAsync(NotificationTypesEnum typesEnum, int dataId, int eventOwner);
    }
}