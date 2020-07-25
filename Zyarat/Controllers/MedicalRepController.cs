using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Data;
using Zyarat.Models.Services.IdentityServices;
using Zyarat.Models.Services.MedicalRepService;
using Zyarat.Resources;
using Zyarat.Responses.MedicalRepResponses;

namespace Zyarat.Controllers
{
    [ApiController]
    [Route("api/rep")]
    public class MedicalRepController:Controller
    {
        private readonly IMedicalRepService _service;
        private readonly IMapper _mapper;
        public MedicalRepController(IMedicalRepService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetAllReps")]
        public async Task<IActionResult> GetAllReps([FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state = await _service.GetAll(pageNumber, pageSize);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }
            
            return Ok(state.Source.Select(rep => new
            {
                rep.City.CityName,
                rep.Id,
                rep.IdentityUser.Email ,
                rep.IdentityUser.UserName,
                rep.IdentityUser.PhoneNumber,
                rep.FName,
                rep.LName,
                rep.LikeCount,
                rep.UniqueUsers,
                rep.VisitsCount,
                rep.ProfileUrl,
                rep.DisLikeCount,
                rep.WorkedOnCompany,
            }));
        }

        [HttpGet("{repId}")]
        public async Task<IActionResult> GetRep(int repId)
        {
            var state = await _service.GetRepAsync(repId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            var rep = _mapper.Map<MedicalRep, GetMedicalRepResponse>(state.Source);
            
            //add unMapped properties
            rep.Email = state.Source.IdentityUser.Email;
            rep.UserName = state.Source.IdentityUser.UserName;
            rep.Phone = state.Source.IdentityUser.PhoneNumber;
            //end of adding unMapped
            
            return Ok(rep);
        }
        /**
         * https://localhost:5001/api/rep/
         */

    
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]AddMedicalRepResourcesRequest request)
        {
            var state=await _service.AddRepAsync(rep: request);
            if (!state.Success)
            {
                return BadRequest(state.Errors);
            }
            return Ok(new TokenAndRefresh
            {
                Token = state.Token,
                RefreshToken = state.RefreshToken
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email,[FromForm]string password)
        {
            var state = await _service.Login(email, password);
            if (!state.Success ||!state.Source.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(new TokenAndRefresh
            {
                Token = state.Source.Token,
                RefreshToken = state.Source.RefreshToken
            });

        }
        

        [HttpPut("UpdateImageProfile/{repId}")]
        public async Task<IActionResult> UpdateImageProfile(int repId,[FromForm]IFormFile file)
        {
            var state = await _service.UpdateImageProfile(repId,file);
            if (!state.Success) return BadRequest(state.Error);
            var rep = _mapper.Map<MedicalRep, GetMedicalRepResponse>(state.Source);
            
            //add unMapped properties
            rep.Email = state.Source.IdentityUser.Email;
            rep.UserName = state.Source.IdentityUser.UserName;
            rep.Phone = state.Source.IdentityUser.PhoneNumber;
            //end of adding unMapped
            
            return Ok(rep);
        }
        
        [HttpPut("UpdatePhone/{repId}/{phone}")]
        /**
         * https://localhost:5001/api/rep/UpdatePhone/31/01123124356
         */
        public async Task<IActionResult> UpdatePhone(int repId,string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length != 11)
                return BadRequest("Invalid Phone Format");
            
            var state=await _service.Modify(repId, rep1 => rep1.IdentityUser.PhoneNumber = phone);
            if (!state.Success)
            {
                return BadRequest(state.Source);
            }

            var rep = _mapper.Map<MedicalRep, GetMedicalRepResponse>(state.Source);
            
            //add unMapped properties
            rep.Email = state.Source.IdentityUser.Email;
            rep.UserName = state.Source.IdentityUser.UserName;
            rep.Phone = state.Source.IdentityUser.PhoneNumber;
            //end of adding unMapped
            
            return Ok(rep);
        }
        
        [HttpPut("UpdateCompany/{repId}/{company}")]
        /**
         * https://localhost:5001/api/rep/UpdateCompany/31/makramComapny
         */
        public async Task<IActionResult> UpdateCompany(int repId,string company)
        {
            if (string.IsNullOrEmpty(company))
                return BadRequest("Invalid Company Format");
            
            var state=await _service.Modify(repId, rep1 => rep1.WorkedOnCompany=company);
            if (!state.Success)
            {
                return BadRequest(state.Source);
            }

            var rep = _mapper.Map<MedicalRep, GetMedicalRepResponse>(state.Source);
            
            //add unMapped properties
            rep.Email = state.Source.IdentityUser.Email;
            rep.UserName = state.Source.IdentityUser.UserName;
            rep.Phone = state.Source.IdentityUser.PhoneNumber;
            //end of adding unMapped
            return Ok(rep);
        }
        
        [HttpPut("UpdatePosition/{repId}/{positionId}")]
        public async Task<IActionResult> UpdatePosition(int repId,int positionId)
        {
            if (positionId==0)
                return BadRequest("Invalid Position !");
            
            var state=await _service.Modify(repId, rep1 => rep1.MedicalRepPositionId=positionId);
            if (!state.Success)
            {
                return BadRequest(state.Source);
            }

            var rep = _mapper.Map<MedicalRep, GetMedicalRepResponse>(state.Source);
            
            //add unMapped properties
            rep.Email = state.Source.IdentityUser.Email;
            rep.UserName = state.Source.IdentityUser.UserName;
            rep.Phone = state.Source.IdentityUser.PhoneNumber;
            //end of adding unMapped
            
            return Ok(rep);
        }

        [HttpGet("GetUnActiveUsers")]
        public async Task<IActionResult> GetUnActiveUsers([FromQuery]int pageNumber,[FromQuery]int pageSize)
        {
            var state = await _service.GetUnActiveUsersAsync(pageNumber, pageSize);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<IEnumerable<MedicalRep>,IEnumerable<GetUnActiveUsersResponse>>(state.Source));
        }

        [HttpPut("active/{repId}")]
        public async Task<IActionResult> Active(int repId)
        {
            var state = await _service.ActiveUser(repId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<MedicalRep, GetUnActiveUsersResponse>(state.Source));
        }
        
        [HttpPut("DeletePermanently/{repId}")]
        public async Task<IActionResult> DeletePermanently(int repId)
        {
            var state = await _service.DeleteUserPermanently(repId);
            if (!state.Success)
            {
                return BadRequest(state.Error);
            }

            return Ok(_mapper.Map<MedicalRep, GetUnActiveUsersResponse>(state.Source));
        }
        
        
    }
    
}