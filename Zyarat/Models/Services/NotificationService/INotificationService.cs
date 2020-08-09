using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Factories;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.NotificationService
{
    public interface INotificationService
    {
        Task<Response<EventNotification>> AddEventNotificationAsync(NotificationTypesEnum typesEnum,int senderId,int dataId);
        Task<Response<Message>> AddAMessageAsync(string  content,int receiverId);
        Task<Response<IEnumerable<Message>>> AddMessagesAsync(string content, List<int> receiversIds);
        Task<Response<GlobalMessage>> AddGlobalMessageAsync(string content);
        Response<IEnumerable<EventNotification>> GetEventNotification(int pageNumber, int pageSize);
        Response<IEnumerable<TotalMessage>> GetAllMessages(int repId, int pageNumber, int pageSize);
    }
}