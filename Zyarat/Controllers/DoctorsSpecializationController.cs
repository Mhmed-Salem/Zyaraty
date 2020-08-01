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
    public class DoctorsSpecializationController:Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public DoctorsSpecializationController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<MedicalSpecialized>, List<MedicalSpecializationDto>>(
                await _context.MedicalSpecializeds.ToListAsync()));
        }
    }
}