using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Handlers.NotificationHandlers;
using Zyarat.Models.Factories;
using Zyarat.Models.Repositories.EvaluationRepos;

namespace Zyarat.Models.Repositories.NotificationRepo
{
    public class NotificationRepo:ContextRepo,INotificationRepo
    {
        private readonly IEvaluationRepo _evaluationRepo;

        public NotificationRepo(ApplicationContext context, IEvaluationRepo evaluationRepo) : base(context)
        {
            _evaluationRepo = evaluationRepo;
        }

        public async Task AddEventNotificationAsync(EventNotification notification)
        {
            await Context.AddAsync(notification);
        }

        public  int CountUnReadMessages(int repId)
        {
            var r = Context.CountMessages.FromSqlInterpolated($"exec GetUnReadMessages @repId={repId}").ToList()
                .FirstOrDefault();
            if (r==null) throw new Exception("Error!");
            return r.Count;

        }

        public async Task<int>  CountUnReadEvents(int repId)
        {
            return await Context.EventNotifications.CountAsync(message => !message.Read);
        }

        public async  Task ReadGlobals(List<int> globals, int readerId)
        {
            await Context.GlobalMessageReading.AddRangeAsync(globals.Select(i => new GlobalMessageReading
            {
                ReaderId = readerId,
                GlobalMessageId = i
            }));
        }

        public  GlobalMessageReading GetGlobalReading(int globalId, int repId)
        {
            return  Context.GlobalMessageReading.FirstOrDefault(reading => reading.ReaderId == repId
                                                                                     && reading.GlobalMessageId ==
                                                                                     globalId);
        }


        public async Task<EventNotification> GetEvent(NotificationTypesEnum typesEnum, int dataId)
        {
            return await Context.EventNotifications
                .Include(notification => notification.NotificationType)
                .FirstOrDefaultAsync(
                notification => notification.DataId==dataId && notification.NotificationTypeId==(int)typesEnum);
        }

        public async Task<EventNotification> GetEvent(int dataId,int typeId)
        {
            return await Context.EventNotifications
                .Include(notification => notification.NotificationType)
                .FirstOrDefaultAsync(
                    notification => notification.DataId==dataId&&typeId==notification.NotificationTypeId);
            
        }

        public async Task<EventNotification> DeleteEvent(int dataId, int typeId)
        {
            var ev = await GetEvent(dataId, typeId);
            if (ev==null)
            {
                return null;
            }
            Context.EventNotifications.Remove(ev);
            return ev;
        }

        public async  Task<Message> GetMessage(int messageId)
        {
            return await Context.Messages.Include(message => message.Content)
                .ThenInclude(content => content.NotificationType)
                .FirstOrDefaultAsync(message => message.Id==messageId);
        }

        public async  Task<GlobalMessage> GetGlobalMessage(int globalId)
        {
            return await Context.GlobalMessages.Include(message => message.MessageContent)
                .ThenInclude(content => content.NotificationType)
                .FirstOrDefaultAsync(message => message.Id==globalId);
        }


        public async Task AddAMessageAsync(Message message,int receiverId)
        {
            await Context.AddAsync(message);
        }
        public async Task AddMessagesAsync(IEnumerable<Message> messages)
        {
            await Context.AddRangeAsync(messages);
        }

        public async  Task AddGlobalMessageAsync(GlobalMessage globalMessage)
        {
            await Context.AddAsync(globalMessage);
        }
        
        public IEnumerable<EventNotification> GetEventsNotifications(int repId,int pageNumber, int pageSize)
        {
            return  Context.EventNotifications
                .Include(notification => notification.NotificationType)
                .Where (notification => notification.MedicalRepId==repId)
                .OrderByDescending(notification => notification.DateTime)
                .Skip((pageNumber-1)*pageSize).Take(pageSize);
        }

        public IEnumerable<TotalMessage> GetAllMessages(int repId, int pageNumber, int pageSize)
        {
            return Context.GlobalMessages
                .Include(message => message.MessageContent)
                .Select(message => new TotalMessage
                {
                    Content = message.MessageContent.Content,
                    Id = message.Id,
                    DateTime = message.DateTime,
                    TypesEnum = (NotificationTypesEnum) message.MessageContent.NotificationTypeId
                })
                .Union(Context.Messages
                    .Include(message => message.Content.Content)
                    .Where(message =>message.ReceiverId==repId )
                    .Select(message => new TotalMessage
                    {
                        Content = message.Content.Content,
                        DateTime = message.DateTime,
                        Id = message.Id,
                        TypesEnum =(NotificationTypesEnum) message.Content.NotificationTypeId
                    }))
                .OrderByDescending(message => message.DateTime)
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize);
        }

        public List<GlobalMessage> GetGlobalMessages(int pageNumber, int pageSize)
        {
            var data = Context.GlobalMessages
                .Include(message => message.MessageContent)
                .OrderByDescending(te => te.DateTime)
                .Skip((pageNumber-1)*pageSize).Take(pageSize);
            return data.ToList();
        }

        public List<Message> GetMessages(int repId, int pageNumber, int pageSize)
        {
           return  Context.Messages
                .Include(message => message.Content)
                .Where(message => message.ReceiverId == repId)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

      
    }
}