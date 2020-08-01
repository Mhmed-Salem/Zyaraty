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
        private const int LimitsOfReportsAfterWhichTheVisitWillBeUnActive = 2;
        private readonly IVisitsRepo _visitsRepo;
        public async Task HandleAddingVisitAsync(Visit visit)
        {
            var rep =await GetUserAsyncWithAllHisInfo(visit.MedicalRepId);
            rep.VisitsCount++;
        }
        
        

        public async Task HandleRemovingVisitAsync(Visit visit)
        {
            var rep =await GetUserAsyncWithAllHisInfo(visit.MedicalRepId);
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

        public void HandleReportingVisit(Visit visit, IEnumerable<VisitReport> report)
        {
            if (report.Count()>=LimitsOfReportsAfterWhichTheVisitWillBeUnActive)
            {
                _visitsRepo.UnActiveVisit(visit);
            }
        }

     
        public MedicalRepVisitsHandlers(ApplicationContext context, IVisitsRepo visitsRepo) : base(context)
        {
            _visitsRepo = visitsRepo;
        }
    }
}