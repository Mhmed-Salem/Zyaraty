using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.IVisitService.VisitsServices
{
    public interface IVisitService
    {
        Task<Response<Visit>> AddVisit(AddVisitContract contract);
        Task<Response<IEnumerable<GetVisitByDoctorDto>>> GetVisitByDoctor(int doctorId, int userId);
        Task<Response<IEnumerable<GetVisitByCityDto>>> GetVisitByCity(int cityId, int userId);
        Task<Response<Visit>> GetVisitAsync(int visitId);
        bool IsActiveComment(Visit visit);
        Task<Response<Visit>> GetVisitReportsAsync(int visitId);

    }
}