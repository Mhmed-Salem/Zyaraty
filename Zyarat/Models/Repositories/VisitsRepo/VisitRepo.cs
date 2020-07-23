using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;


namespace Zyarat.Models.Repositories.VisitsRepo
{
    public class VisitRepo : ContextRepo, IVisitsRepo
    {
        
        public async Task<Visit> GetVisit(int visitId)
        {
            return await Context.Visits.FindAsync(visitId);
        }

        public async Task<Visit> GetVisitWithOwner(int visitId)
        {
            return await Context.Visits.Include(v => v.MedicalRep).
                FirstOrDefaultAsync(visit => visit.Id == visitId);
        }

        public async Task AddVisitAsync(Visit visit)
        {
           await Context.Visits.AddAsync(visit);
        }

        
        public void DeleteVisit(Visit visit)
        {
            Context.Visits.Remove(visit);
        }

     

        public IEnumerable<Visit> GetVisitOfDoctorAsync(int doctorId, int userId)
        {
            return  Context.Visits
                .Include(visit => visit.Doctor)
                .Include(visit => visit.MedicalRep)
                .ThenInclude(rep => rep.IdentityUser)
                .Include(visit => visit.Evaluation)
                .Where(visit => visit.DoctorId == doctorId);
            
        }
        public IEnumerable<Visit> GetLatestInCityAsync(int cityId, int userId)
        {
            return Context.Visits
                .Include(visit => visit.MedicalRep)
                .ThenInclude(rep => rep.IdentityUser)
                .Include(visit => visit.MedicalRep.City)
                .Include(visit => visit.Doctor)
                .Include(visit => visit.Evaluation)
                .Where(visit => visit.MedicalRep.CityId == cityId);
        }

        public void RemoveVisitsRange(List<Visit> visits)
        {
            Context.Visits.RemoveRange(visits);
        }

        public VisitRepo(ApplicationContext context) : base(context)
        {
        }
    }
}