using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Zyarat.Data;
using Zyarat.Helpers;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.RequestResponseInteracting;
using Zyarat.Models.Services.IdentityServices;
using Zyarat.Resources;

namespace Zyarat.Models.Services.MedicalRepService
{
    public class MedicalRepService : FileService, IMedicalRepService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMedicalRepRepo _repo;
        private readonly IMapper _mapper;
        private IIdentityUser _identityUser;

        public MedicalRepService(
            IUnitWork unitWork,
            IMedicalRepRepo repo,
            IWebHostEnvironment environment,
            IMapper mapper,
            IIdentityUser identityUser) : base(environment)
        {
            _unitWork = unitWork;
            _repo = repo;
            _mapper = mapper;
            _identityUser = identityUser;
        }

        public async Task<RegisterServiceResult> AddRepAsync(AddMedicalRepResourcesRequest request)
        {
            try
            {
                var rep = _mapper.Map<AddMedicalRepResourcesRequest, MedicalRep>(request);
                if (request.Image != null && request.Image.Length > 0)
                {
                    rep.ProfileUrl = await UploadAsync(request.Image);
                }

                var identity = new IdentityUser
                {
                    Email = request.Email,
                    PhoneNumber = request.Phone,
                    UserName = rep.GenerateUserName()
                };
                var result = await _identityUser.RegisterAsync(identity, request.Password);
                rep.IdentityUser = identity;
                await _repo.AddUser(rep);
                await _unitWork.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                return new RegisterServiceResult($"Can not Register :{e.InnerException}");
            }
        }


        public async Task<Response<List<MedicalRep>>> GetAll(int page, int pageCount)
        {
            try
            {
                var list = _repo.GetAllUsersAsync(page, pageCount);
                await _unitWork.CommitAsync();
                return new Response<List<MedicalRep>>(list);
            }
            catch (Exception e)
            {
                return new Response<List<MedicalRep>>($"Can not get the data :{e.Message}");
            }
        }

        public async Task<Response<MedicalRep>> Modify(int id, Update update)
        {
            try
            {
                var user = await GetRepAsync(id);
                if (!user.Success)
                {
                    return user;
                }

                update(user.Source);
                await _unitWork.CommitAsync();
                return new Response<MedicalRep>(user.Source);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Error: {e.Message}");
            }
        }


        public async  Task<Response<MedicalRep>> UpdateImageProfile(int id, IFormFile formFile)
        {
            var rep = await _repo.GetUserAsync(id);
            var newUrl=await UpdateFileAsync(rep.ProfileUrl, formFile);
            if (newUrl==null)
            { 
                return new Response<MedicalRep>("Error can not update Image profile");
            }
            rep.ProfileUrl = newUrl;
            await _unitWork.CommitAsync();
            return new Response<MedicalRep>(rep);
        }

        public async Task<Response<MedicalRep>> DeleteRepAsync(int id)
        {
            try
            {
                var user = await _repo.GetUserAsync(id);
                if (user == null)
                {
                    return new Response<MedicalRep>("User does  Not Exist");
                }

                _repo.UnActive(user);
                await _unitWork.CommitAsync();
                return new Response<MedicalRep>(user);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Can not delete the user :{e.Message}");
            }
        }

        
        public async Task<Response<MedicalRep>> GetRepAsync(int repId)
        {
            try
            {
                var user = await _repo.GetUserAsync(repId);
                return user == null ? new Response<MedicalRep>("User does  Not Exist") : new Response<MedicalRep>(user);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Error:{e.Message}");
            }
        }
        
        
    }
}


