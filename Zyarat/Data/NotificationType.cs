using System.Collections.Generic;
using Zyarat.Models.Factories;

namespace Zyarat.Data
{
    public class NotificationType
    {
        public NotificationType()
        {
            Notifications=new List<EventNotification>();
            MessageContents=new List<MessageContent>();
        }
        public NotificationTypesEnum Type { set; get; }
        public string Template { set; get; }
        public List<EventNotification> Notifications { set; get; }
        public List<MessageContent> MessageContents;

    }
}