using Zyarat.Data;
using Zyarat.Models.Services.IVisitService.VisitsServices;

namespace Zyarat.Handlers
{
    public class VisitAssertion
    {
        private readonly IVisitService _visitService;

        public VisitAssertion(IVisitService visitService)
        {
            _visitService = visitService;
        }

        public bool AssertActivation(Visit visit)
        {
            return _visitService.IsActiveComment(visit);
        }

       
    }
}