using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.ReportServices
{
    public interface IReportService
    {
        Task<Response<VisitReport>> AddReport(VisitReport visitReport);
    }
}