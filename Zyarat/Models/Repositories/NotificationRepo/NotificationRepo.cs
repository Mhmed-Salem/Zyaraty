using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
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

        public async Task<EventNotification> GetEvent(NotificationTypesEnum typesEnum, int dataId)
        {
            return await Context.EventNotifications.FirstOrDefaultAsync(
                notification => notification.DataId==dataId && notification.NotificationTypeId==(int)typesEnum);
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
        
        public IEnumerable<EventNotification> GetEventsNotifications(int pageNumber, int pageSize)
        {
            return  Context.EventNotifications.
                OrderByDescending(notification => notification.DateTime)
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

        public IEnumerable<GlobalMessage> GetGlobalMessages(int pageNumber, int pageSize)
        {
            var data = Context.GlobalMessages
                .Include(message => message.MessageContent)
                .OrderByDescending(te => te.DateTime)
                .Skip((pageNumber-1)*pageSize).Take(pageSize);
            return data;
        }

        public IEnumerable<Message> GetMessages(int repId, int pageNumber, int pageSize)
        {
           return  Context.Messages
                .Include(message => message.Content)
                .Where(message => message.ReceiverId == repId)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

      
    }
}