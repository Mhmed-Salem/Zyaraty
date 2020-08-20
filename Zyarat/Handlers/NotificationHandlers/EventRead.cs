using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.KeysObjs;

namespace Zyarat.Handlers.NotificationHandlers
{
    public class EventRead:IReadable<EventNotificationPrimaryKey>
    {
        private readonly EventNotification _eventNotification;

        public EventRead(EventNotification eventNotification)
        {
            _eventNotification = eventNotification;
        }

        public EventNotificationPrimaryKey Read()
        {
            _eventNotification.Read = true;
            return new EventNotificationPrimaryKey(_eventNotification.DataId,_eventNotification.NotificationTypeId);
        }

        public bool IsRead()
        {
            return _eventNotification.Read;
        }
    }
}