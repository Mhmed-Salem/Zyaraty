using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Models.Services.IVisitService;
using Zyarat.Models.Services.IVisitService.VisitsServices;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VisitController:Controller
    {
        private readonly IVisitService _service;
        private readonly IMapper _mapper;
        

        public VisitController(IVisitService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetVisitsInDoctor/{doctorId}/{userId}")]
        public async Task<IActionResult> GetVisitsInDoctor(int doctorId,int userId)
        {
            var state =  await _service.GetVisitByDoctor(doctorId, userId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source);
        }
        
     [HttpGet("GetVisitsInCity/{cityId}/{userId}")]
        public async Task<IActionResult> GetVisitsInCity(int cityId,int userId)
        {
            var state =  await _service.GetVisitByCity(cityId, userId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddVisitContract contract)
        {
            var state = await _service.AddVisit(contract);
            if (!state.Success)
                BadRequest(state.Error);
            return Ok(_mapper.Map<Visit,AddVisitContract>(state.Source));
        }
    }
}