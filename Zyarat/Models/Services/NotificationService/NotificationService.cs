using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Factories;
using Zyarat.Models.Factories.MessageFactory;
using Zyarat.Models.Factories.NotificationFactory;
using Zyarat.Models.Repositories.NotificationRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.NotificationService
{
    public class NotificationService:INotificationService
    {
        private readonly IUnitWork _unitWork;
        private readonly INotificationRepo _repo;
        private readonly INotificationEventFactory _notificationFactory;
        private readonly IGlobalMessageFactory _globalMessageFactory;
        private readonly IMessageFactory _messageFactory;

        public NotificationService(
            IUnitWork unitWork,
            INotificationRepo repo,
            INotificationEventFactory notificationFactory,
            IGlobalMessageFactory globalMessageFactory,
            IMessageFactory messageFactory)
        {
            _unitWork = unitWork;
            _repo = repo;
            _notificationFactory = notificationFactory;
            _globalMessageFactory = globalMessageFactory;
            _messageFactory = messageFactory;
        }


        public async Task<Response<EventNotification>> AddEventNotificationAsync(NotificationTypesEnum typesEnum, int senderId, int dataId)
        {
            try
            {
                if (await _repo.GetEvent(typesEnum,dataId)!=null)
                {
                    return new Response<EventNotification>($"it's already Exists !'");
                }
                var notification = await _notificationFactory.CreateAsync(typesEnum,dataId: dataId, senderId);
                await _repo.AddEventNotificationAsync(notification);
                await _unitWork.CommitAsync();
                return new Response<EventNotification>(notification);
            }
            catch (Exception e)
            {
                return new Response<EventNotification>($"ERROR: {e.Message}");
            }
        }

        public async Task<Response<Message>> AddAMessageAsync(string content,int receiverId)
        {
            try
            {
                var message = _messageFactory.CreateAMessage(content, receiverId);
                await _repo.AddAMessageAsync(message,receiverId);
                await _unitWork.CommitAsync();
                return new Response<Message>(message);
            }
            catch (Exception e)
            {
              return new Response<Message>($"ERROR :{e.Message}");
            }
        }

     

        public async  Task<Response<IEnumerable<Message>>> AddMessagesAsync(string content, List<int> receiversIds)
        {
            try
            {
                var messages = _messageFactory.CreateMultiMessages(content, receiversIds);
                await _repo.AddMessagesAsync(messages);
                await _unitWork.CommitAsync();
                return new Response<IEnumerable<Message>>(messages);
            }
            catch (Exception e)
            {
                return new  Response<IEnumerable<Message>>($"ERROR :{e.Message}");
            }
        }

        public async  Task<Response<GlobalMessage>> AddGlobalMessageAsync(string content)
        {
            try
            {
                var global = _globalMessageFactory.Create(content);
                await _repo.AddGlobalMessageAsync(global);
                await _unitWork.CommitAsync();
                return new Response<GlobalMessage>(global);
            }
            catch (Exception e)
            {
                return new  Response<GlobalMessage>($"ERROR :{e.Message}");
            }
        }


        public Response<IEnumerable<EventNotification>>GetEventNotification(int pageNumber, int pageSize)
        {
            try
            {
                var list =  _repo.GetEventsNotifications(pageNumber, pageSize).ToList();
                //Reading Logic
                foreach (var eventNotification in list)
                {
                    if (!eventNotification.Read)
                    {
                        eventNotification.Read = true;
                    }
                    else break;
                }
                //end of reading logic 
                return new Response<IEnumerable<EventNotification>>(list);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<EventNotification>>($"ERROR :{e.Message}");
            }
        }

        public Response<IEnumerable<TotalMessage>> GetAllMessages(int repId,int pageNumber, int pageSize)
        {
            try
            {
                var messages = _repo.GetMessages(repId, pageNumber, pageSize);
                var global = _repo.GetGlobalMessages(pageNumber, pageSize);

                var all = messages.Select(message => new TotalMessage 
                    {
                        Content = message.Content.Content,
                        Id = message.Id,
                        DateTime = message.DateTime,
                        TypesEnum = (NotificationTypesEnum) message.Content.NotificationTypeId,
                    })
                    .Union(global.Select(globalm => new TotalMessage
                    {
                        Content = globalm.MessageContent.Content,
                        Id = globalm.Id,
                        DateTime = globalm.DateTime,
                        TypesEnum = (NotificationTypesEnum) globalm.MessageContent.NotificationTypeId,
                    }))
                    .OrderBy(message => message.DateTime)
                    .Skip((pageNumber-1)*pageSize).Take(pageSize);
                
                return new Response<IEnumerable<TotalMessage>>(all);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<TotalMessage>>($"ERROR :{e.Message}");
            }
        }
    }
}