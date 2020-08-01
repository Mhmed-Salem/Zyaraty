using System.Collections.Generic;
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
    public class GovController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GovController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var govs = await _context.Governments.Include(government => government.Cities).ToListAsync();
            return Ok(_mapper.Map<List<Government>, List<GovernmentDto>>(govs));
        }
    }
}