using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.Services.ReportServices;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    
    public class ReportController:Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async  Task<IActionResult> AddReport([FromQuery]int visitId,[FromQuery]int reporterId)
        {
            var state = await _reportService.AddReport(new VisitReport
            {
                VisitId = visitId,
                ReporterId = reporterId
            });
            if (!state.Success)
            {
                return BadRequest(state.Error);
            } 
            return Ok(new
            {
                Id=state.Source.Id,
                VisitId=state.Source.VisitId,
                ReporterId=state.Source.ReporterId
            });
        }
        
    }
}