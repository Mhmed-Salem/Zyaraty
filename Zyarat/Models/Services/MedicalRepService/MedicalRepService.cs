using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Zyarat.Controllers.Hubs;
using Zyarat.Data;
using Zyarat.Helpers;
using Zyarat.Models.DTO;
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
        private readonly IIdentityUser _identityUser;

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
                await _repo.AddUser(rep);
                var identity = new IdentityUser
                {
                    Email = request.Email,
                    PhoneNumber = request.Phone,
                    UserName = rep.GenerateUserName()
                };
                var result = await _identityUser.RegisterAsync(identity, request.Password,rep);
                rep.IdentityUser = identity;
                await _unitWork.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                return new RegisterServiceResult($"Can not Register :{e.InnerException}");
            }
        }

        public int GetOnlineUsersCount()
        {
            return ClientsRepo.Users.Count;
        }

        public async  Task<Response<IEnumerable<MedicalRepSearchResult>>> Search(string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return new Response<IEnumerable<MedicalRepSearchResult>>("Query is empty");
                }
                var split = query.Split(" ");
                if (split[0].Length<3)
                {
                    return new Response<IEnumerable<MedicalRepSearchResult>>($"Length should be more than 3 characters ");
                }


                return split.Length == 1 ||string.IsNullOrWhiteSpace(split[1])
                    ? new Response<IEnumerable<MedicalRepSearchResult>>(await _repo.Search(split[0]))
                    : new Response<IEnumerable<MedicalRepSearchResult>>(await _repo.Search(split[0], split[1]));

            }
            catch (Exception e)
            {
                return new Response<IEnumerable<MedicalRepSearchResult>>($"Error:{e.Message}");
            }
        }

        public async Task<RegisterServiceResult> AddRepForTestAsync(AddMedicalRepResourcesRequest request)
        {
            try
            {
                var rep = _mapper.Map<AddMedicalRepResourcesRequest, MedicalRep>(request);
             

                var identity = new IdentityUser
                {
                    Email = request.Email,
                    PhoneNumber = request.Phone,
                    UserName = rep.GenerateUserName()
                };
                var result = await _identityUser.RegisterAsync(identity, request.Password,rep);
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
            var rep = await _repo.GetUserAsyncWithAllHisInfo(id);
            var newUrl=await UpdateFileAsync(rep.ProfileUrl, formFile);
            if (newUrl==null)
            { 
                return new Response<MedicalRep>("Error can not update Image profile");
            }
            rep.ProfileUrl = newUrl;
            await _unitWork.CommitAsync();
            return new Response<MedicalRep>(rep);
        }

        public async  Task<Response<IEnumerable<MedicalRep>>> GetUnActiveUsersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var source = await _repo.GetUnActiveUsersAsync(pageNumber,pageSize);
                return new Response<IEnumerable<MedicalRep>>(source);
            }
            catch (Exception e)
            {
              return new Response<IEnumerable<MedicalRep>>($"Error :{e.Message}");
            }
        }

        public async Task<Response<MedicalRep>> ActiveUser(int repId)
        {
            try
            {
                var user = await _repo.GetUserAsyncWithAllHisInfo(repId);
                _repo.ActiveUser(user);
                await _unitWork.CommitAsync();
                return new Response<MedicalRep>(user);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Error: {e.Message}");
            }
        }

        public async Task<Response<RegisterServiceResult>> RefreshTokensAsync(string token, string refreshToken)
        {
            try
            {
                var reply = await _identityUser.RefreshTokenAsync(token, refreshToken);
                return new Response<RegisterServiceResult>(reply);
            }
            catch (Exception e)
            {
                return new Response<RegisterServiceResult>($"Error: {e.Message}");
            }
        }

        public async Task<Response<RegisterServiceResult>> Login(string email, string password)
        {
            try
            {
                var reply = await _identityUser.LoginAsync(email, password);
                return new Response<RegisterServiceResult>(reply);
            }
            catch (Exception e)
            {
                return new Response<RegisterServiceResult>($"Error:{e.Message}");
            }
        }

        public async Task<Response<MedicalRep>> DeleteUserPermanently(int repId)
        {
            try
            {
                var user = await _repo.GetUserAsyncWithAllHisInfo(repId);
                _repo.DeleteUserPermanently(user);
                await _unitWork.CommitAsync();
                return new Response<MedicalRep>(user);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Error :{e.Message}");
            }
        }

        public async Task<Response<MedicalRep>> DeleteRepAsync(int id)
        {
            try
            {
                var user = await _repo.GetUserAsyncWithAllHisInfo(id);
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
                var user = await _repo.GetUserAsyncWithAllHisInfo(repId);
                return user == null ? new Response<MedicalRep>("User does  Not Exist") : new Response<MedicalRep>(user);
            }
            catch (Exception e)
            {
                return new Response<MedicalRep>($"Error:{e.Message}");
            }
        }
    }
}


