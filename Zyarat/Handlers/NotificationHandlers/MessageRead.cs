using Zyarat.Data;

namespace Zyarat.Handlers.NotificationHandlers
{
    public class MessageRead:IReadable<int>
    {
        private Message _message;

        public MessageRead(Message message)
        {
            _message = message;
        }

        public int Read()
        {
            _message.Read = true;
            return _message.Id;
        }

        public bool IsRead() => _message.Read;
    }
}