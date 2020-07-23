using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.VisitsRepo;

namespace Zyarat.Handlers
{
 

    public class MedicalRepVisitsHandlers:MedicalRepRepo
    {
        public async Task HandleAddingVisitAsync(Visit visit)
        {
            var rep =await GetUserAsync(visit.MedicalRepId);
            rep.VisitsCount++;
        }

        public async Task HandleRemovingVisitAsync(Visit visit)
        {
            var rep =await GetUserAsync(visit.MedicalRepId);
            rep.VisitsCount--;
            
        }
        /// <summary>
        /// days the comment will be deleted after
        /// </summary>
        private const int VisitLife = 30;

        public IEnumerable<Visit> HandleDeleting(IEnumerable<Visit> visits)
        {
            var removedList=new List<Visit>();
            var rtList=new List<Visit>();
            foreach (var visit in visits)
            {
                if (visit.DateTime.AddDays(VisitLife)<DateTime.Now)
                {
                    removedList.Add(visit);
                }
                else 
                    rtList.Add(visit);
            }

            return rtList;
        }

     
        public MedicalRepVisitsHandlers(ApplicationContext context) : base(context)
        {
     
        }
    }
}