using System;
using Zyarat.Data;

namespace Zyarat.Models.Factories.MessageFactory
{
    public class GlobalMessageFactory:IGlobalMessageFactory
    {
        public GlobalMessage Create(string content)
        {
            return new GlobalMessage
            {
                DateTime = DateTime.Now,
                MessageContent = new MessageContent
                {
                    Content = content,
                    NotificationTypeId = (int) NotificationTypesEnum.GlobalMessage
                }
            };
        }
    }
}