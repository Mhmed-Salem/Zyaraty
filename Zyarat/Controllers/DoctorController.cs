using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zyarat.Contract.Doctor;
using Zyarat.Data;
using Zyarat.Models.DTO;
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
        
        [HttpGet("GetDoctor/{doctorId}")]
        public async Task<IActionResult> GetDoctor([FromRoute]int doctorId)
        {
            var data=await _context.Doctors
                .Include(doctor => doctor.City)
                .ThenInclude(city => city.Government)
                .Include(doctor => doctor.MedicalSpecialized)
                .FirstOrDefaultAsync(doctor => doctor.Id==doctorId);
            if (data==null)
            {
                return BadRequest($"Error:No doctor with id = {doctorId}");
            }

            return Ok(_mapper.Map<Doctor,DoctorDto>(data));
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

        class SearchO
        {
            public int Id { set; get; }
            public string Name { set; get; }
            public string Gov { set; get; }
            public string City { set; get; }
            public string Spec{ set; get; }
        }

        [HttpGet("Search")]
        public IActionResult Search([FromQuery]string query)
        {
            var split = query.ToLower().Split(" ");
            if (string.IsNullOrEmpty(split[0])||split[0].Length<3)
            {
                return NoContent();
            }

            if (split.Length == 1)
            {
                return Ok(
                    _context.Doctors
                        .Include(rep => rep.City)
                        .ThenInclude(city => city.Government)
                        .Include(doctor => doctor.MedicalSpecialized)
                        .Where(rep => EF.Functions.Like(rep.FName,$"%{split[0]}%"))
                        .Select(doctor => new SearchO
                        {
                            Id = doctor.Id,
                            Name = doctor.FName + " " + doctor.LName,
                            Gov = doctor.City.Government.Gov,
                            City = doctor.City.CityName,
                            Spec = doctor.MedicalSpecialized.Type
                        }).ToList()
                );
            } 
            return Ok(
                _context.Doctors
                .Include(rep => rep.City)
                .ThenInclude(city => city.Government)
                .Include(doctor => doctor.MedicalSpecialized)
                .Where(rep => rep.FName.Equals(split[0]) &&EF.Functions.Like(rep.LName,$"%{split[1]}%"))
                .Select(doctor => new SearchO
                {
                    Id = doctor.Id,
                    Name = doctor.FName + " " + doctor.LName,
                    Gov = doctor.City.Government.Gov,
                    City = doctor.City.CityName,
                    Spec = doctor.MedicalSpecialized.Type
                }).ToList());
        }
    }
}
