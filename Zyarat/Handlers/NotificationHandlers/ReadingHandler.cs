using System.Collections.Generic;
using System.Linq;

namespace Zyarat.Handlers.NotificationHandlers
{
    public class ReadingHandler<T>:IReadingHandler<T>
    {
        protected List<IReadable<T>> _readables;

        public virtual List<T> ReadAll()
        {
            return (from readable in _readables where !readable.IsRead() select readable.Read()).ToList();
        }

        public virtual void SetObj(List<IReadable<T>> obj)
        {
            _readables = obj;
        }
    }
}