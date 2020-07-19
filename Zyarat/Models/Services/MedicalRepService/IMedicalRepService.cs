using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Helpers;
using Zyarat.Models.RequestResponseInteracting;
using Zyarat.Resources;

namespace Zyarat.Models.Services.MedicalRepService
{
    public interface IMedicalRepService
    {
        
        Task<RegisterServiceResult> AddRepAsync(AddMedicalRepResourcesRequest rep);
        Task<Response<List<MedicalRep>>> GetAll(int page ,int pageCount);
        Task<Response<MedicalRep>> DeleteRepAsync(int id);
        Task<Response<MedicalRep>> GetRepAsync(int repId);
        Task<Response<MedicalRep>> Modify(int id, Update update);
        Task<Response<MedicalRep>> UpdateImageProfile(int id,IFormFile url);

    }
}