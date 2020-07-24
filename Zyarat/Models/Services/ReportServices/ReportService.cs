using System;
using System.Linq;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Models.Repositories.ReportRepo;
using Zyarat.Models.RequestResponseInteracting;
using Zyarat.Models.Services.InterActing;

namespace Zyarat.Models.Services.ReportServices
{
    public class ReportService:IReportService
    {
        private readonly IReportRepo _repo;
        private readonly IVisitInteracting _interacting;
        private readonly VisitAssertion _assertion;
        private readonly MedicalRepVisitsHandlers _visitsHandlers;
        private readonly MedicalRepReportHandlers _reportHandlers;
        private readonly IUnitWork _unitWork;
        

        public ReportService(IReportRepo repo,
            IVisitInteracting interacting,
            VisitAssertion assertion,
            MedicalRepVisitsHandlers visitsHandlers,
            MedicalRepReportHandlers reportHandlers,
            IUnitWork unitWork)
        {
            _repo = repo;
            _interacting = interacting;
            _assertion = assertion;
            _visitsHandlers = visitsHandlers;
            _reportHandlers = reportHandlers;
            _unitWork = unitWork;
        }

        public async Task<Response<VisitReport>> AddReport(VisitReport visitReport)
        {
            try
            {
                var associatedVisit = await _interacting.GetVisitWithItsReportsAsync(visitReport.VisitId);
                if (!associatedVisit.Success)
                {
                    return new Response<VisitReport>(associatedVisit.Error);
                }

                if (associatedVisit.Source==null)
                {
                     return new Response<VisitReport>("Visit Is not found!");
                }

                if (! _assertion.AssertActivation(associatedVisit.Source))
                {
                    return new Response<VisitReport>("You try to report to deleted or Un Active Comment !");
                }

                if (associatedVisit.Source.VisitReports.Any(report => report.ReporterId==visitReport.ReporterId))
                {
                    return new Response<VisitReport>("User already has made a Report to this visit!");
                }

                if (associatedVisit.Source.Type)
                {
                    return new Response<VisitReport>("Report Can only applied for the Optional Visits !");
                }
                
                
                associatedVisit.Source.VisitReports.Add(visitReport);
                
                _visitsHandlers.HandleReportingVisit(associatedVisit.Source,associatedVisit.Source.VisitReports);
                await _reportHandlers.HandleReporting(associatedVisit.Source, associatedVisit.Source.VisitReports);
                await _unitWork.CommitAsync();
                return new Response<VisitReport>(visitReport);
            }
            catch (Exception e)
            {
                return new Response<VisitReport>($"Error:{e.Message}");
            }
        }
    }
}