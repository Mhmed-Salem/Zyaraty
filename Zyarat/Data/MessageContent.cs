namespace Zyarat.Data
{
    public class MessageContent
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public NotificationType NotificationType { set; get; }
        public int NotificationTypeId { set; get; }
    }
}