using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Factories;

namespace Zyarat.Models.Repositories.NotificationRepo
{
    public class NotificationTypeRepo:ContextRepo,INotificationTypeRepo
    {
        public async Task<NotificationType> Get(NotificationTypesEnum type)
        {
            return await Context.NotificationTypes.FindAsync(type);
        }

        public NotificationTypeRepo(ApplicationContext context) : base(context)
        {
        }
    }
}