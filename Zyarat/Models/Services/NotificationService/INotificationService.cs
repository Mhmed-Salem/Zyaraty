using System;
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
        Task<int> CountUnReadEvents(int repId);
        int CountUnReadMessages(int repId);
        Task<Response<EventNotification>> GetEvent(int dataId, int typeId);
        Task<Response<EventNotification>> DeleteEvent(int dataId, int typeId);

        Task<Response<Message>> GetMessage(int messageId);
        Task<Response<TotalMessage>> GetGlobalMessage(int messageId, int repId);
        Task<Response<EventNotification>> AddEventNotificationAsync(EventNotification notification);
        Task<Response<Message>> AddAMessageAsync(string  content,int receiverId);
        Task<Response<IEnumerable<Message>>> AddMessagesAsync(string content, List<int> receiversIds);
        Task<Response<GlobalMessage>> AddGlobalMessageAsync(string content);
        Task<Response<IEnumerable<EventNotification>>> GetEventNotification(int repId, int pageNumber, int pageSize);
        
        Task<Response<IEnumerable<TotalMessage>>> GetAllMessages(int repId, int pageNumber, int pageSize);
    }
}