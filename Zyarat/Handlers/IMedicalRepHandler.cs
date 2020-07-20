using System.Threading.Tasks;
using Zyarat.Data;

namespace Zyarat.Handlers
{
    public interface IMedicalRepHandler
    {

        Task HandleAddingVisit(Visit visit);
         Task RemoveVisitAsync(Visit visit);

    }
}