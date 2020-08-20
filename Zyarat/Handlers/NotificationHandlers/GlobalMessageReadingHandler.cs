using System.Collections.Generic;
using Zyarat.Models.Repositories.NotificationRepo;

namespace Zyarat.Handlers.NotificationHandlers
{
    public class GlobalMessageReadingHandler:ReadingHandler<int>
    {
        private readonly INotificationRepo _notificationRepo;
        private readonly int _repId;

        public GlobalMessageReadingHandler(INotificationRepo notificationRepo, int repId)
        {
            _notificationRepo = notificationRepo;
            _repId = repId;
        }

        public override List<int> ReadAll()
        {
            var l= base.ReadAll();
            _notificationRepo.ReadGlobals(l, _repId);
            return l;
        }
    }
}