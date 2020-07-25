using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Models.Services.MedicalRepService;
using Zyarat.Resources;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/Controller")]
    public class Test:Controller
    {
        private readonly IMedicalRepService _medicalRepService;

        public Test(IMedicalRepService medicalRepService)
        {
            _medicalRepService = medicalRepService;
        }

        [HttpPost("/load")]
        public async Task< IActionResult> AddLoadToReps()
        {
            for (int i = 10300; i < 500000; i++)
            {
                var o= await _medicalRepService.AddRepForTestAsync(new AddMedicalRepResourcesRequest
                {
                    Email = $"eng{i}amaeae@gmail.com",
                    Password = "engamaeae@15304560a",
                    Phone = "01011051728",
                    WorkedOnCompany = "cvwsmvklnsdvlnsdnsvnlks",
                    MedicalRepPositionId = new Random().Next(1, 5),
                    FName = $"ahmed{i}",
                    CityId = new Random().Next(1, 5),
                    LName = $"Embaby{i}",
                });
                Thread.Sleep(100);
            }

            return Ok();
        }
    }
}