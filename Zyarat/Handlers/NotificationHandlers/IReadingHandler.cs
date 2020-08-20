using System.Collections.Generic;

namespace Zyarat.Handlers.NotificationHandlers
{
    public interface IReadingHandler<T>
    {
        List<T> ReadAll();
        void SetObj(List<IReadable<T>> obj);
    }
}