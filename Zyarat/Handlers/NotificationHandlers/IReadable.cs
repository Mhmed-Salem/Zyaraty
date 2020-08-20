using System.Threading.Tasks;

namespace Zyarat.Handlers.NotificationHandlers
{
    //T is the Id of the IReadable element
    public interface IReadable<out T>
    {
        /// <summary>
        /// Set the element as read one and return its Id
        /// </summary>
        /// <returns></returns>
         T Read();

        bool IsRead();
    }
}