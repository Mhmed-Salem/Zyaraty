using Zyarat.Data;

namespace Zyarat.Models.Factories.MessageFactory
{
    public interface IGlobalMessageFactory
    {
        GlobalMessage Create(string content);
    }
}