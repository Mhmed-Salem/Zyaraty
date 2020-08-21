using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.CompilerServices;
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

            var state = await _service.AddNextCompetition(new Competition
            {
                Roles = source.Roles,
                Type = t,
                MinUniqueUsers = source.MinUniqueUsers,
                MinUniqueVisits = source.MinUniqueVisits,
            });
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<Competition,CompetitionDto>(state.Source));
        }
    
    
        [HttpPost("testAdd/{type}")]
        public async  Task<IActionResult> AddNextTest1([FromRoute]string type,[FromForm]CompetitionDto source)
        {
            if (source.MinUniqueUsers<0||source.MinUniqueVisits<0)
            {
                return BadRequest("Error :either the unique users or unique visits is negative");
            }
            bool t ;
            if (type.ToLower().Equals("daily"))
                t = false;
            else if (type.ToLower().Equals("monthly"))
                t = true;
            else return BadRequest($"there is not Competition Type named {type} ");
          
            
            var state = await _service.AddNextCompetition_Test(new Competition
            {
                Roles = source.Roles,
                Type = t,
                MinUniqueUsers = source.MinUniqueUsers,
                MinUniqueVisits = source.MinUniqueVisits,
                DateTime = DateTime.Now
            },new DateTime(2020,8,22,2,1,1));
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(state.Source);
        }
        
        
        [HttpPut("{type}")]
        public async Task<IActionResult> Modify([FromRoute]string type,[FromForm]CompetitionDto source)
        {
            bool t ;
            if (type.ToLower().Equals("daily"))
                t = false;
            else if (type.ToLower().Equals("monthly"))
                t = true;
            else return BadRequest($"there is not Competition Type named {type} ");
            var compel = _mapper.Map<CompetitionDto, Competition>(source);
            compel.Type = t;
            var state = await _service.ModifyNextCompetition(compel);
            if(!state.Success)
                return BadRequest(state.Error);
            return Ok(_mapper.Map<Competition,CompetitionDto>(state.Source));
        }
        [HttpPut("ModifyTest/{type}")]
        public async Task<IActionResult> Modify_Test([FromRoute]string type,[FromForm]CompetitionDto source)
        {
            bool t ;
            if (type.ToLower().Equals("daily"))
                t = false;
            else if (type.ToLower().Equals("monthly"))
                t = true;
            else return BadRequest($"there is not Competition Type named {type} ");
            var compel = _mapper.Map<CompetitionDto, Competition>(source);
            compel.Type = t;
            var state = await _service.ModifyNextCompetition_Test(compel,
                new DateTime(2020,8,23,2,3,14));
            if(!state.Success)
                return BadRequest(state.Error);
            return Ok(_mapper.Map<Competition,CompetitionDto>(state.Source));
        }
        
        [HttpGet("GetMonthlyFinalResult")]
        public async Task<IActionResult> GetMonthlyFinalResult([FromQuery]int year,[FromQuery]int month)
        {
            var state =  await _service.GetFinalResult(CompetitionType.Monthly,year: year, month: month,day: 1);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            var winners = state.Source;
            return Ok(winners);
        }
        
        [HttpGet("GetDailyFinalResult")]
        public async Task<IActionResult> GetDailyFinalResult([FromQuery]int year,[FromQuery]int month,int day)
        {
            var state =  await _service.GetFinalResult(CompetitionType.Daily,year: year, month: month,day: day);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }
            var winners = state.Source;
            return Ok(winners);
        }
        
        [HttpGet("{type}")]
        public async  Task<IActionResult> GetCurrentResult([FromRoute] string type)
        {
            CompetitionType competitionType;

            if (type.ToLower().Equals("monthly"))
            {
                competitionType = CompetitionType.Monthly;
            }
            
            else if (type.ToLower().Equals("daily"))
            {
                competitionType = CompetitionType.Daily;
            }
            else return BadRequest($"no Type named {type} ");

            var state = await _service.GetCurrentResult(competitionType,
                IntegerType.FromString(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type=="id")?.Value));
            if(!state.Success)
                return BadRequest(state.Error);
            return Ok(state.Source);
        }

        [HttpGet("getNext/{type}")]
        public async Task<IActionResult> GetNext([FromRoute]string type)
        {
            CompetitionType competitionType;

            if (type.ToLower().Equals("monthly"))
            {
                competitionType = CompetitionType.Monthly;
            }
            
            else if (type.ToLower().Equals("daily"))
            {
                competitionType = CompetitionType.Daily;
            }
            else return BadRequest($"no Type named {type} ");

            var state = await _service.GetNextCompetition(competitionType);
            if (!state.Success)
            {
                return NoContent();
            }

            return Ok (_mapper.Map<Competition, CompetitionDto>(state.Source));
        }
        
        [HttpGet("GetHackers/{type}/{limit}")]
        public IActionResult GetHeroes([FromRoute]string type,[FromRoute]int limit)
        {
            CompetitionType competitionType;

            if (type.ToLower().Equals("monthly"))
            {
                competitionType = CompetitionType.Monthly;
            }
            
            else if (type.ToLower().Equals("daily"))
            {
                competitionType = CompetitionType.Daily;
            }
            else return BadRequest($"no Type named {type} ");

            var state =  _service.GetHackers(competitionType, limit);
            if (!state.Success)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<IEnumerable<CompetitionHacker>,IEnumerable<CompetitionWinnerForUser>>(state.Source));
        }
        
    }
}