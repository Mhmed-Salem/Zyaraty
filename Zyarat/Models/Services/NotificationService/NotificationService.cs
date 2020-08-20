using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Data.KeysObjs;
using Zyarat.Handlers.NotificationHandlers;
using Zyarat.Models.Factories;
using Zyarat.Models.Factories.MessageFactory;
using Zyarat.Models.Repositories.NotificationRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.NotificationService
{
    public class NotificationService:INotificationService
    {
        private readonly IUnitWork _unitWork;
        private readonly INotificationRepo _repo;
        private readonly IGlobalMessageFactory _globalMessageFactory;
        private readonly IMessageFactory _messageFactory;

        public NotificationService(
            IUnitWork unitWork,
            INotificationRepo repo,
            IGlobalMessageFactory globalMessageFactory,
            IMessageFactory messageFactory)
        {
            _unitWork = unitWork;
            _repo = repo;
            _globalMessageFactory = globalMessageFactory;
            _messageFactory = messageFactory;
        }


        public async Task<int>  CountUnReadEvents(int repId)
        {
            return  await _repo.CountUnReadEvents(repId);
        }

        public int CountUnReadMessages(int repId)
        {
            return  _repo.CountUnReadMessages(repId);
        }

        public async Task<Response<EventNotification>> GetEvent(int dataId, int typeId)
        {
            try
            {
                var ev = await _repo.GetEvent(dataId,typeId);
                if (ev==null)
                {
                    return new Response<EventNotification>("Not found");
                }
                var readHandler=new ReadingHandler<EventNotificationPrimaryKey>();
                var arr = new List<IReadable<EventNotificationPrimaryKey>> {new EventRead(ev)};
                readHandler.SetObj(arr);
                var readEvent=readHandler.ReadAll();
                await _unitWork.CommitAsync();

                if (readEvent.Count>0)
                {
                    ev.Read = false;
                }

                return new Response<EventNotification>(ev);
            }
            catch (Exception e)
            {
               return new Response<EventNotification>($"ERROR :{e.Message}");
            }
        }

        public async  Task<Response<EventNotification>> DeleteEvent(int dataId, int typeId)
        {
            try
            {
                var ev = await _repo.DeleteEvent(dataId, typeId);
                if (ev==null)
                {
                    throw new InputFormatterException($"Not found an Event with dataId={dataId} and typeId={typeId}");
                }
                await _unitWork.CommitAsync();
                return new Response<EventNotification>(ev);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Response<Message>> GetMessage(int messageId)
        {
            try
            {
                var msg =await  _repo.GetMessage(messageId);
                var readHandler=new ReadingHandler<int>();
                var arr = new List<IReadable<int>> {new MessageRead(msg)};
                readHandler.SetObj(arr);
                var readEvent=readHandler.ReadAll();
                await _unitWork.CommitAsync();
                if (readEvent.Count>0)
                {
                    msg.Read = false;
                }

                return msg == null
                    ? new Response<Message>("Error :Message Is not found ")
                    : new Response<Message>(msg);
            }
            catch (Exception e)
            {
              return new Response<Message>($"Error :{e.Message}");
            }
        }

        public async Task<Response<TotalMessage>> GetGlobalMessage(int messageId,int repId)
        {
            try
            {
                var msg =await  _repo.GetGlobalMessage(messageId);
                if (msg==null)
                {
                    return new Response<TotalMessage>("Error :Message Is not found ");
                }
                var gMsg = new TotalMessage
                {
                    Content =msg.MessageContent.Content,
                    Id = msg.Id,
                    DateTime = msg.DateTime,
                    TypesEnum =NotificationTypesEnum.GlobalMessage
                };
                var handler=new GlobalMessageRead(msg,_repo,repId);
                var readingHandler=new GlobalMessageReadingHandler(_repo,repId);
                readingHandler.SetObj(new List<IReadable<int>>{handler});
                var readGlobal = readingHandler.ReadAll();
                await _unitWork.CommitAsync();

                if (readGlobal.Any())
                {
                    gMsg.Read = false;
                }

                return new Response<TotalMessage>(gMsg);
            }
            catch (Exception e)
            {
                return new Response<TotalMessage>($"Error :{e.Message}");
            }        
        }


        public async Task<Response<EventNotification>> AddEventNotificationAsync(EventNotification eventNotification)
        {
            try
            {
                await _repo.AddEventNotificationAsync(eventNotification);
                await _unitWork.CommitAsync();
                return new Response<EventNotification>(eventNotification);
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


        /// <summary>
        /// Get the event notifications in pagination format .
        /// then ,it will set the returned data (event notification) as read .
        /// </summary>
        /// <param name="repId"></param>
        /// <param name="pageNumber">for pagination</param>
        /// <param name="pageSize">for pagination</param>
        /// <returns></returns>
        public async Task<Response<IEnumerable<EventNotification>>>GetEventNotification(int repId,int pageNumber, int pageSize)
        {
            try
            {
                var dictionary =  _repo.GetEventsNotifications(repId,pageNumber, pageSize)
                    .ToDictionary(notification => new EventNotificationPrimaryKey(notification.DataId,notification.NotificationTypeId));
                //Reading Logic
                var r = new ReadingHandler<EventNotificationPrimaryKey>();
                var obj = new List<IReadable<EventNotificationPrimaryKey>>(dictionary.Select(notification => new EventRead(notification.Value)));
                r.SetObj(obj);
                var readElements = r.ReadAll();//the Ids that were influenced in reading process.
                //end of reading logic 
                await _unitWork.CommitAsync();
                /**
                 * after commiting ,the data is set as read ,but the client
                 * need to know the whether the received data is read or not.
                 * so we reset th data to the state it was in before commiting .
                 */
                foreach (var element in readElements)
                {
                    dictionary[new EventNotificationPrimaryKey(element.DataId,element.TypeId)].Read = false;
                }
                return new Response<IEnumerable<EventNotification>>(dictionary.Values);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<EventNotification>>($"ERROR :{e.Message}");
            }
        }


        public async Task<Response<IEnumerable<TotalMessage>>> GetAllMessages(int repId,int pageNumber, int pageSize)
        {
            try
            {
                var messages =   _repo.GetMessages(repId, pageNumber, pageSize);
                var global = _repo.GetGlobalMessages(pageNumber, pageSize);
                var all = messages.Select(message => new TotalMessage 
                    {
                        Content = message.Content.Content,
                        Id = message.Id,
                        DateTime = message.DateTime,
                        TypesEnum = (NotificationTypesEnum) message.Content.NotificationTypeId,
                    })
                    .Union(global.Select(globals => new TotalMessage
                    {
                        Content = globals.MessageContent.Content,
                        Id = globals.Id,
                        DateTime = globals.DateTime,
                        TypesEnum = (NotificationTypesEnum) globals.MessageContent.NotificationTypeId,
                    }))
                    .OrderBy(message => message.DateTime)
                    .Skip((pageNumber-1)*pageSize).Take(pageSize)
                    .ToDictionary(message => new MessageKey
                    {
                        Id = message.Id,
                        Type =(int) message.TypesEnum
                    }); 
                
                /**
                 * Handling Reading for both types 
                 */
                var messageReading=new ReadingHandler<int>();
                messageReading.SetObj(new List<IReadable<int>>(messages
                    .Select(message => new MessageRead(message))));
                var messageRead=messageReading.ReadAll();
                
                var globalHandler=new GlobalMessageReadingHandler(_repo,repId);
                globalHandler.SetObj(new List<IReadable<int>>(
                    global.Select(message => new GlobalMessageRead(message,_repo,repId))));
                var globalRead=globalHandler.ReadAll();
                
                //end of reading
                foreach (var message in messageRead)
                {
                    all[new MessageKey{Type =(int) NotificationTypesEnum.Message, Id = message}].Read = false;
                }
                
                foreach (var message in globalRead)
                {
                    all[new  MessageKey{Type = (int)NotificationTypesEnum.Message, Id = message}].Read = false;
                }
                
                await _unitWork.CommitAsync();
                return new Response<IEnumerable<TotalMessage>>(all.Values);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<TotalMessage>>($"ERROR :{e.Message}");
            }
        }
    }
}