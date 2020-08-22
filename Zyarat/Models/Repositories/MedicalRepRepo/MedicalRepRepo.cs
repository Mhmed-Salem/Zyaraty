using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.RequestResponseInteracting;

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

        public async Task<List<MedicalRepSearchResult>> Search([NotNull]string fname,string lname="")
        {
            if (string.IsNullOrEmpty(lname))
            {
                var x = await Context.MedicalReps.AsNoTracking()
                    .Include(rep => rep.IdentityUser)
                    .Include(rep => rep.City)
                    .ThenInclude(city => city.Government)
                    .Where(rep => !rep.PermanentDeleted &&
                                  EF.Functions.Like(rep.FName, $"%{fname}%"))
                    .Select(rep => new MedicalRepSearchResult
                    {
                        Id = rep.Id,
                        Name = rep.FName + " " + rep.LName,
                        UserName = rep.IdentityUser.UserName,
                        Active = rep.Active,
                        City = rep.City.CityName,
                        Gov = rep.City.Government.Gov,
                        ProfileUrl = rep.ProfileUrl,
                    }).ToListAsync();
                return x;
            }
            return await Context.MedicalReps.AsNoTracking()
                .Include(rep => rep.IdentityUser)
                .Include(rep => rep.City)
                .ThenInclude(city => city.Government)
                .Where(rep => !rep.PermanentDeleted 
                              && rep.FName.Equals(fname) 
                              &&EF.Functions.Like(rep.LName, $"%{lname}%"))
                .Select(rep => new MedicalRepSearchResult
                {
                    Id = rep.Id,
                    Name = rep.FName + " " + rep.LName,
                    UserName = rep.IdentityUser.UserName,
                    Active = rep.Active,
                    City = rep.City.CityName,
                    Gov = rep.City.Government.Gov,
                    ProfileUrl = rep.ProfileUrl,
                }).ToListAsync();
            
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