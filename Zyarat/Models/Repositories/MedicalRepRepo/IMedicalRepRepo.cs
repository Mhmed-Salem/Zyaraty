using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.DTO;

namespace Zyarat.Models.Repositories.MedicalRepRepo
{
    public interface IMedicalRepRepo
    {
        Task<MedicalRep> GetUserAsyncWithAllHisInfo(int id);
        Task<List<MedicalRepSearchResult>> Search(string fname, string lname = "");
        Task<MedicalRep> GetUserByIdentityIdAsync(string identityId);

        List<MedicalRep> GetAllUsersAsync(int page ,int itemsCount);
        MedicalRep ModifyUser(MedicalRep old, MedicalRep newMedicalRep);
        Task<MedicalRep>  AddUser(MedicalRep medicalRep);
        void UnActive(MedicalRep rep);
        Task<MedicalRep> GetVisitOwnerAsync(int visitId);
        Task<IEnumerable<MedicalRep>> GetUnActiveUsersAsync(int pageNumber, int pageSize);
        void ActiveUser(MedicalRep rep);
        
        void DeleteUserPermanently(MedicalRep medicalRep);
    }
}