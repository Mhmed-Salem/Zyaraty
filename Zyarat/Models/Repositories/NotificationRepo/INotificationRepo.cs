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
        Task<EventNotification> GetEvent(NotificationTypesEnum typesEnum, int dataId);
        Task AddAMessageAsync(Message message,int receiverId);
        Task AddMessagesAsync(IEnumerable<Message> messages);
        Task AddGlobalMessageAsync(GlobalMessage globalMessage);
        IEnumerable<EventNotification> GetEventsNotifications(int pageNumber, int pageSize);
        IEnumerable<Message> GetMessages(int repId, int pageNumber, int pageSize);
        IEnumerable<TotalMessage> GetAllMessages(int repId,int pageNumber,int pageSize);
        IEnumerable<GlobalMessage> GetGlobalMessages(int pageNumber, int pageSize);

    }
}