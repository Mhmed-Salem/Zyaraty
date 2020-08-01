using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Zyarat.Contract.Doctor;
using Zyarat.Data;
using Zyarat.Migrations;
using Zyarat.Responses;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DoctorController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public DoctorController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddDoctorDto doctorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Root.Errors.ToString());
            }

            var doctor = _mapper.Map<AddDoctorDto, Doctor>(doctorDto);
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<Doctor,DoctorResponse>(doctor));
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetDoctorsInACity(int cityId)
        {
            var doctors = await _context.Doctors.Where(doctor => doctor.CityId == cityId)
                .Select(doctor => new
                {
                    doctor.Id,
                    doctor.FName,
                    doctor.LName
                }).ToListAsync();
            return Ok(doctors);
        }
    }
}
