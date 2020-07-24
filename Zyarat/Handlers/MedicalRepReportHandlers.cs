using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Repositories.MedicalRepRepo;

namespace Zyarat.Handlers
{
    public class MedicalRepReportHandlers
    {
        private const int  LimitOfReportsTheRepWillDeactivatedAfter=2;
        private readonly IMedicalRepRepo _medicalRepRepo;

        public MedicalRepReportHandlers(IMedicalRepRepo medicalRepRepo)
        {
            _medicalRepRepo = medicalRepRepo;
        }

        public async Task HandleReporting(Visit visit, IEnumerable<VisitReport> reports)
        {
            if (reports.Count()>=LimitOfReportsTheRepWillDeactivatedAfter)
            {
                _medicalRepRepo.UnActive(await _medicalRepRepo.GetUserAsync(visit.MedicalRepId));
            }
        }
    }
}