using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zyarat.Contract.Doctor;
using Zyarat.Data;
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
                .Include(doctor => doctor.MedicalSpecialized)
                .Select(doctor => new AddDoctorDto
                {
                    CityId = doctor.CityId,
                    FName = doctor.FName,
                    LName = doctor.LName,
                    MedicalSpecializedId = doctor.MedicalSpecializedId,
                    AdderMedicalRepId = doctor.AdderMedicalRepId
                }).ToListAsync();
            return Ok(doctors);
        }
        [HttpGet("Getall")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var data = await _context.Doctors.AsNoTracking()
                .Include(doctor => doctor.City)
                .ThenInclude(city => city.Government)
                .Include(doctor => doctor.MedicalSpecialized)
                .Include(doctor => doctor.AdderMedicalRep)
                .Select(doctor => new
                {
                    city = new
                    {
                        id = doctor.City.Id,
                        cityName = doctor.City.CityName,
                        gov = doctor.City.Government.Gov
                    },
                    id = doctor.Id,
                    fname = doctor.FName,
                    lname = doctor.LName,
                    spechialized = new
                    {
                        id = doctor.MedicalSpecialized.Id,
                        type = doctor.MedicalSpecialized.Type
                    },
                    adder = new
                    {
                        doctor.AdderMedicalRep.Id,
                        doctor.AdderMedicalRep.FName,
                        doctor.AdderMedicalRep.LName
                    }
                })
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return Ok(data);
        }
    }
}
