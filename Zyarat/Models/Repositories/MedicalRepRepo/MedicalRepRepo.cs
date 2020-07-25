using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;

namespace Zyarat.Models.Repositories.MedicalRepRepo
{
    public class MedicalRepRepo:ContextRepo,IMedicalRepRepo
    {
      

        public MedicalRepRepo(ApplicationContext context) : base(context)
        {
            
        }

        public async Task<MedicalRep> GetUserAsyncWithAllHisInfo(int id)
        {
            return await Context.MedicalReps
                .Include(rep => rep.IdentityUser)
                .Include(rep =>rep.City )
                .Include(rep => rep.MedicalRepPosition)
                .Where(rep => !rep.PermanentDeleted)
                .FirstOrDefaultAsync(rep => rep.Id == id);
        }

        public async Task<MedicalRep> GetUserByIdentityIdAsync(string identityId)
        {
            return await Context.MedicalReps.Include(rep => rep.IdentityUser)
                .Where(rep => !rep.PermanentDeleted)
                .Where(rep => rep.IdentityUser.Id == identityId).FirstOrDefaultAsync();
        }

        public  List<MedicalRep> GetAllUsersAsync(int page, int itemsCount)
        {
            return  Context.MedicalReps.AsNoTracking()
                .Include(rep => rep.IdentityUser)
                .Include(rep =>rep.City )
                .Include(rep => rep.MedicalRepPosition)
                .Where(rep => !rep.PermanentDeleted)
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

        public async Task<IEnumerable<MedicalRep>> GetUnActiveUsersAsync(int pageNumber, int pageSize)
        {
            return await  Context.MedicalReps.Where(rep => !rep.Active && !rep.PermanentDeleted)
                .AsNoTracking()
                .Where(rep => !rep.PermanentDeleted)
                .OrderByDescending(rep => rep.DeActiveDate).Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }

        public void ActiveUser(MedicalRep rep)
        {
            rep.Active = true;
        }

        public void DeleteUserPermanently(MedicalRep medicalRep)
        {
            medicalRep.PermanentDeleted = true;
        }
    }
}