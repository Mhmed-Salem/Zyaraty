using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Repositories.VisitsRepo;

namespace Zyarat.Handlers
{
    public class DeletingVisitsHandler:VisitRepo
    {
        /// <summary>
        /// days the comment will delete after
        /// </summary>
        private const int VisitLife = 30;

        public List<Visit> HandleDeleting(IEnumerable<Visit> visits)
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

        public DeletingVisitsHandler(ApplicationContext context) : base(context)
        {
        }
    }
}