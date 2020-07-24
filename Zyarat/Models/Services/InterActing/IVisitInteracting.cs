using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.InterActing
{
    public interface IVisitInteracting
    {
        Task<Response<Visit>> GetVisitAsync(int visitId);
        Task<Response<Visit>> GetVisitWithItsReportsAsync(int visitId);
        
    }
}