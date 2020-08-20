using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Factories;

namespace Zyarat.Models.Repositories.NotificationRepo
{
    public interface INotificationRepo
    {
        Task AddEventNotificationAsync(EventNotification notification);
        int CountUnReadMessages(int repId);
        Task<int> CountUnReadEvents(int repId);
        Task ReadGlobals(List<int> globals,int readerId);
        GlobalMessageReading GetGlobalReading(int globalId,int repId);
        Task<EventNotification> GetEvent(NotificationTypesEnum typesEnum, int dataId);
        Task<EventNotification> GetEvent(int dataId,int typeId);
        Task<EventNotification> DeleteEvent(int dataId, int typeId);
        Task<Message> GetMessage( int messageId);
        Task<GlobalMessage> GetGlobalMessage(int globalId);
        Task AddAMessageAsync(Message message,int receiverId);
        Task AddMessagesAsync(IEnumerable<Message> messages);
        Task AddGlobalMessageAsync(GlobalMessage globalMessage);
        IEnumerable<EventNotification> GetEventsNotifications(int repId,int pageNumber, int pageSize);
        List<Message> GetMessages(int repId, int pageNumber, int pageSize);
        IEnumerable<TotalMessage> GetAllMessages(int repId,int pageNumber,int pageSize);
        List<GlobalMessage> GetGlobalMessages(int pageNumber, int pageSize);

    }
}