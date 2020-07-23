using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.InterActing
{
    public class VisitInteracting:IVisitInteracting
    {
        private IVisitService.VisitsServices.IVisitService _visitService;

        public VisitInteracting(IVisitService.VisitsServices.IVisitService visitService)
        {
            _visitService = visitService;
        }

        public async Task<Response<Visit>> GetVisitAsync(int visitId)
        {
            return await _visitService.GetVisitAsync(visitId);
        }
    }
}