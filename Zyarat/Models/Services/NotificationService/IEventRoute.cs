using System.Threading.Tasks;
using Zyarat.Models.Factories;

namespace Zyarat.Models.Services.NotificationService
{
    public interface  IEventRoute
    {
        Task<object> Route(NotificationTypesEnum typesEnum,int dataId);
    }
}