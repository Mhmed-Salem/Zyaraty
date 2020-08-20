using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Repositories.NotificationRepo;

namespace Zyarat.Models.Factories.NotificationFactory
{
    public abstract class  EventBuilder
    {
        protected readonly INotificationTypeRepo _notificationTypeRepo;

        protected EventBuilder(INotificationTypeRepo notificationTypeRepo)
        {
            _notificationTypeRepo = notificationTypeRepo;
        }

        public  abstract Task<EventNotification> Build();
    }
}