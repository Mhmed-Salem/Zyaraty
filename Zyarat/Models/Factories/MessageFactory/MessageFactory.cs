using System;
using System.Collections.Generic;
using System.Linq;
using Zyarat.Data;

namespace Zyarat.Models.Factories.MessageFactory
{
    public class MessageFactory:IMessageFactory
    {
        public Message CreateAMessage(string content, int receiverId)
        {
            return new Message
            {
                ReceiverId = receiverId,
                Content = new MessageContent
                {
                    Content = content,
                    NotificationTypeId = (int) NotificationTypesEnum.Message
                },
                DateTime = DateTime.Now
            };
        }

        public List<Message> CreateMultiMessages(string contents, List<int> receiversIds)
        {
            var contentIbj = new MessageContent
            {
                Content = contents,
                NotificationTypeId = (int) NotificationTypesEnum.Message
            };

            return receiversIds.Select(repId => new Message {Content = contentIbj, DateTime = DateTime.Now, ReceiverId = repId}).ToList();
        }
    }
}