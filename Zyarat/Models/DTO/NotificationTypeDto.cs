using Zyarat.Models.Factories;

namespace Zyarat.Models.DTO
{
    public class NotificationTypeDto
    {
        public NotificationTypesEnum Type;
        public string Template { set; get; }
    }
}