using System.Collections.Generic;

namespace Zyarat.Data
{
    public class NotificationType
    {
        public NotificationType()
        {
            GlobalMessages=new List<GlobalMessage>();
            Notifications=new List<Notification>();
        }
        public int Id { set; get; }
        public string Type { set; get; }
        public string Template { set; get; }
        public List<GlobalMessage> GlobalMessages { set; get; }
        public List<Notification> Notifications { set; get; }
        
    }
}