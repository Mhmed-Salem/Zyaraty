using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.Services.CompetitionService;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CompetitionController:Controller
    {
        private readonly ICompetitionService _service;
        private readonly IMapper _mapper;

        public CompetitionController(ICompetitionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        
        
        [HttpPost("{type}")]
        public async  Task<IActionResult> AddNext([FromRoute]string type,[FromForm]CompetitionDto source)
        {
            bool t ;
            if (type.ToLower().Equals("daily"))
                t = false;
            else if (type.ToLower().Equals("monthly"))
                t = true;
            else return BadRequest($"there is not Competition Type named {type} ");

            var state = await _service.AddNextCompetition(new Competition()
            {
                Roles = source.Roles,
                Type = t,
                MinUniqueUser = source.MinUniqueUsers,
                MinUniqueVisit = source.MinUniqueVisits
            });
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source);
        }
        
        [HttpPut("{type}/{id}")]
        public async Task<IActionResult> Modify([FromRoute]string type,[FromRoute]int id,[FromForm]CompetitionDto source)
        {
            bool t ;
            if (type.ToLower().Equals("daily"))
                t = false;
            else if (type.ToLower().Equals("monthly"))
                t = true;
            else return BadRequest($"there is not Competition Type named {type} ");
            var compel = _mapper.Map<CompetitionDto, Competition>(source);
            compel.Type = t;
            var state = await _service.ModifyNextCompetition(id,compel);
            if(!state.Success)
                return BadRequest(state.Error);
            return Ok(state.Source);
        }
        
        [HttpGet("GetMonthlyFinalResult")]
        public async Task<IActionResult> GetMonthlyFinalResult([FromQuery]int year,[FromQuery]int month)
        {
            var state =  await _service.GetFinalResult(CompetitionType.Monthly,year:year, month:month,1);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source);
        }
        
        [HttpGet("GetDailyFinalResult")]
        public async Task<IActionResult> GetDailyFinalResult([FromQuery]int year,[FromQuery]int month,int day)
        {
            var state =  await _service.GetFinalResult(CompetitionType.Monthly,year:year, month:month,day);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<IEnumerable<Winner>,IEnumerable<WinnerDto>>(state.Source));
        }
        
        [HttpGet("{type}")]
        public async  Task<IActionResult> GetCurrentResult([FromRoute] string type)
        {
            CompetitionType competitionType;
            
            if (type.ToLower().Equals("monthly"))
            {
                competitionType = CompetitionType.Monthly;
            }
            
            else if (type.ToLower().Equals("monthly"))
            {
                competitionType = CompetitionType.Daily;
            }
            else return BadRequest($"no Type named {type} ");

            var state = await _service.GetCurrentResult(competitionType);
            if(!state.Success)
                return BadRequest(state.Error);
            return Ok(state.Source);
        }
        
    
        
        
    }
}