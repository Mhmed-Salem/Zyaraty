using System;
using Zyarat.Models.Factories;

namespace Zyarat.Resources
{
    public class AddGlobalMessageResponse
    {
        public int Id { set; get; }
        public DateTime DateTime { set; get; }
        public int MessageContentId { set; get; }
        public string Content { set; get; }
        public NotificationTypesEnum TypeId{ set; get; } = NotificationTypesEnum.GlobalMessage;
    }
}