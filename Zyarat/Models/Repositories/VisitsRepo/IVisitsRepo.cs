using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.DTO;

namespace Zyarat.Models.Repositories.VisitsRepo
{
    public interface IVisitsRepo
    {
        Task<Visit> GetVisit(int visitId);
        Task<Visit> GetVisitWithOwner(int visitId);

        Task AddVisitAsync(Visit visit);
        void DeleteVisit(Visit visit);
        IEnumerable<Visit> GetVisitOfDoctorAsync(int doctorId, int userId);
        IEnumerable<Visit> GetLatestInCityAsync(int cityId, int userId);
        void RemoveVisitsRange(List<Visit> visits);
        void UnActiveVisit(Visit visit);
        Task<Visit> GetVisitWithItsReportsAsync(int visitId);

    }
}