using System.Collections.Generic;
using Zyarat.Data;

namespace Zyarat.Models.Factories.MessageFactory
{
    public interface IMessageFactory
    {
        Message CreateAMessage(string content,int receiverId);
        List<Message> CreateMultiMessages(string contents,List<int>receiversIds);
    }
}