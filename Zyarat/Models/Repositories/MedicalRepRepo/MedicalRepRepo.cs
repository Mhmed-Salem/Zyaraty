using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;

namespace Zyarat.Models.Repositories.MedicalRepRepo
{
    public class MedicalRepRepo:ContextRepo,IMedicalRepRepo
    {
      

        public MedicalRepRepo(ApplicationContext context) : base(context)
        {
            
        }

        public async Task<MedicalRep> GetUserAsync(int id)
        {
            return await Context.MedicalReps
                .Include(rep => rep.IdentityUser)
                .Include(rep =>rep.City )
                .Include(rep => rep.MedicalRepPosition)
                .FirstOrDefaultAsync(rep => rep.Id == id);
        }

        public  List<MedicalRep> GetAllUsersAsync(int page, int itemsCount)
        {
            return  Context.MedicalReps.AsNoTracking()
                .Include(rep => rep.IdentityUser)
                .Include(rep =>rep.City )
                .Include(rep => rep.MedicalRepPosition)
                .AsEnumerable()
                .Skip((page - 1) * itemsCount).Take(itemsCount).ToList();
        }



        public  MedicalRep ModifyUser(MedicalRep old, MedicalRep newMedicalRep)
        {
            //move data 
            old.City = newMedicalRep.City;
            old.IdentityUser.PhoneNumber = newMedicalRep.IdentityUser.PhoneNumber;
            old.WorkedOnCompany = newMedicalRep.WorkedOnCompany;
            old.CityId = newMedicalRep.CityId;
            old.MedicalRepPosition=newMedicalRep.MedicalRepPosition;
            old.ProfileUrl = newMedicalRep.ProfileUrl;
            //end of moving data
            return old;
        }

        public async Task<MedicalRep> AddUser(MedicalRep medicalRep)
        {
            await Context.MedicalReps.AddAsync(medicalRep);
            return medicalRep;
        }
        
      
        public void  UnActive(MedicalRep rep)
        {
            rep.Active = false;
            rep.DeActiveDate=DateTime.Now;
        }

        public async Task<MedicalRep> GetVisitOwnerAsync(int visitId)
        {
            return await Context.MedicalReps.Include(rep => rep.Visits)
                .FirstOrDefaultAsync(rep => rep.Visits.Any(visit => visit.Id == visitId));
        }
    }
}