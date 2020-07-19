using System.Threading.Tasks;

namespace Zyarat.Models.Services
{
    public interface IUnitWork
    {
        Task CommitAsync();
    }
}