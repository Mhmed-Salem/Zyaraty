using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Zyarat.Data;

namespace Zyarat.Controllers
{
    [ApiController]
    public class TestController:Controller
    {
        private ApplicationContext _context;

        public TestController(ApplicationContext context)
        {
            _context = context;
        }
        
        [HttpPost("/InsertCities")]
        public IActionResult InsertCities()
        {
           
            var l2=new List<City>();
            for (int i = 0; i < 150; i++)
            {
                var r = new Random();
                l2.Add(new City
                {
                    CityName =$"City{i}",
                    GovernmentId = r.Next(34,66)
                });
            }

            _context.Cities.AddRange(l2);
            _context.SaveChanges();
            return Ok();
        }
        
        [HttpPost("/insertReps")]
        public IActionResult InsertReps()
        {
            var r =new Random();
            var a = new MedicalRep[30000];
            for (int i = 0; i < 30000; i++)
            {
                a[i] = new MedicalRep
                {
                    Active = true,
                    CityId = r.Next(152,300),
                    FName = $"ahmed{i}",
                    LName= $"Embaby{i}",
                    ProfileUrl = "ceqmnfcpkmfadlmcla;mscplm;V;PLDSMVLMS;DLVM,L;DMVL;D",
                    WorkedOnCompany = "VDSKANVKLJDSN;AFKJVNKLDSNVKLNCFSVLK; NFD B;LKNCKLN BCKLNVLKLDFSMNVKMSDNVOKNSKDMFNM",
                    MedicalRepPositionId = r.Next(1,4),
                    IdentityUser = new IdentityUser
                    {
                        Email = $"engamaeae{i}@gmail.com",
                        UserName = $"ahmedEmbaby{i}",
                        PhoneNumber = "01011051728",
                    },
                    PermanentDeleted = false,
                };
            }
            _context.MedicalReps.AddRange(a);
            _context.SaveChanges();
            return Ok();
        }
        
        [HttpPost("/addDoctors")]

        public IActionResult AddDoctors()
        {
            var doctors = new Doctor[30000];
            var r=new Random();
            for (int i = 0; i < 30000; i++)
            {
                doctors[i] = new Doctor
                {
                    CityId = r.Next(152, 300),
                    FName = $"ahmed{i}",
                    LName = $"Embaby{i}",
                    MedicalSpecializedId = r.Next(1, 4),
                    AdderMedicalRepId = r.Next(1, 30000),
                };
            }
            _context.Doctors.AddRange(doctors);
            _context.SaveChanges();
            return Ok();
        }
        
        
    }
}