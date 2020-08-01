using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Models.DTO;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MedicalRepPositionController:Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public MedicalRepPositionController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var poss = await _context.MedicalRepPositions.ToListAsync();
            return Ok(_mapper.Map<List<MedicalRepPosition>, List<MedicalRepPositionDto>>(poss));
        }
    }
}