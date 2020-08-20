using System;
using System.Collections.Generic;
using Zyarat.Data;
using Zyarat.Models.Repositories.NotificationRepo;

namespace Zyarat.Handlers.NotificationHandlers
{
    public class GlobalMessageRead:IReadable<int>
    {
        private readonly GlobalMessage _globalMessage;
        private readonly INotificationRepo _notificationRepo;
        private readonly int _repId;
        public GlobalMessageRead(GlobalMessage globalMessage, INotificationRepo notificationRepo, int repId)
        {
            _globalMessage = globalMessage;
            _notificationRepo = notificationRepo;
            _repId = repId;
        }

        public int Read()
        {
            return _globalMessage.Id;
        }

        public bool IsRead()
        {
            return  _notificationRepo.GetGlobalReading(_globalMessage.Id, _repId) != null;
        }
       
    }
}