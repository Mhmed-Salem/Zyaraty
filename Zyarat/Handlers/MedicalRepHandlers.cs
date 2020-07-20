using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Models.Repositories.MedicalRepRepo;

namespace Zyarat.Handlers
{
    public class MedicalRepHandlers:MedicalRepRepo,IMedicalRepHandler
    {
        public async Task HandleAddingVisit(Visit visit)
        {
            var rep = await GetUserAsync(visit.MedicalRepId); 
            Context.MedicalReps.Update(rep);
            rep.VisitsCount++;
        }

        public async Task RemoveVisitAsync(Visit visit)
        {
            var user = await GetUserAsync(visit.MedicalRepId);
            user.VisitsCount--;
        }

      
/**
        public async Task AddEvaluation(int evaluater,)
        {
            var user =await GetUserAsync(evaluated); ;
            if (evaluation.Type)
            {
                user.LikeCount++;
                var result = await Context.Evaluations.

                if (result==null)
                {
                    user.UniqueUsers++;
                }
            }
            else user.DisLikeCount++;

            if ((user.DisLikeCount/(user.DisLikeCount+user.LikeCount)*100)>80)
            {
                DeleteUser(user);
            }
            

        }

        public int RemoveEvaluation(int evaluaterId,Evaluation evaluation)
        {
            
        }
*/
        public MedicalRepHandlers(ApplicationContext context) : base(context)
        {
        }
    }
}