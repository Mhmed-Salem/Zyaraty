using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;

namespace Zyarat.Models.Repositories.MedicalRepRepo
{
    public interface IMedicalRepRepo
    {
        Task<MedicalRep> GetUserAsync(int id);
        List<MedicalRep> GetAllUsersAsync(int page ,int itemsCount);
        MedicalRep ModifyUser(MedicalRep old, MedicalRep newMedicalRep);
        Task<MedicalRep>  AddUser(MedicalRep medicalRep);
        void DeleteUser(MedicalRep rep);
        Task<MedicalRep> GetVisitOwnerAsync(int visitId);
    }
}