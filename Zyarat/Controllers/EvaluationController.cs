using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Services.EvaluationsServices;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class EvaluationController:Controller
    {
        private readonly IEvaluationService _service;
        private readonly IMapper _mapper;

        public EvaluationController(IEvaluationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Modify([FromQuery]int visitId,[FromQuery]int medicalRepId)
        {
            var state =await _service.OppositeEvaluationAsync(medicalRepId,visitId:visitId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<Evaluation,AddEvaluationDto>(state.Source));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEvaluationDto resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Root.Errors);
            var state = await _service.AddEvaluationAsync(resource);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<Evaluation,AddEvaluationDto>(state.Source));
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromQuery]int visitId,[FromQuery]int medicalRepId)
        {
            var state = await _service.RemoveEvaluationAsync(visitId:visitId,medicalRepId);
            
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<Evaluation,AddEvaluationDto>(state.Source));
        }

        [HttpGet]
        public async Task<IActionResult> GetEvaluators(int visitId)
        {
            var state = await _service.GetEvaluatersAsync(commentId:visitId);
            
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<IEnumerable<Evaluation>,IEnumerable<AddEvaluationDto>>(state.Source));
        }
    }
}