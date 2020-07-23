using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Services.EvaluationsServices;
using Zyarat.Resources;
using Zyarat.Responses;
using Zyarat.Responses.MedicalRepResponses;

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

        //well tested
        [HttpPut]
        public async Task<IActionResult> Modify([FromQuery]int visitId,[FromQuery]int medicalRepId)
        {
            var state =await _service.OppositeEvaluationAsync(medicalRepId,visitId:visitId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<Evaluation,AddEvaluationResponse>(state.Source));
        }
        //well tested

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

            return Ok(_mapper.Map<Evaluation,AddEvaluationResponse>(state.Source));
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

        [HttpGet("{visitId}")]

        public async Task<IActionResult> GetEvaluators(int visitId)
        {
            var state = await _service.GetEvaluatorsAsync(commentId:visitId);
            
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source.Select(ev=>new
            {
                Id=ev.Id,
                EvaluatorId=ev.Evaluator.Id,
                Type=ev.Type,
                Name=ev.Evaluator.FName+" "+ev.Evaluator.LName
            }));
        }
    }
}