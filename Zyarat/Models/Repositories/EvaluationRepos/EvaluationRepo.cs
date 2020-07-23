using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Repositories.EvaluationRepos
{
    public class EvaluationRepo:ContextRepo,IEvaluationRepo
    {
        public async Task<IEnumerable<Evaluation>> GetEvaluationsAsync(int visitId)
        {
            return await Context.Evaluations.Where(evaluation => evaluation.VisitId==visitId).ToListAsync();
        }

        public async Task<Evaluation> GetAnEvaluationAsync(int visitId, int userId)
        {
            return await Context.Evaluations.FirstOrDefaultAsync(evaluation =>
                evaluation.VisitId == visitId && evaluation.EvaluatorId == userId);
        }

        public void DeleteEvaluation(Evaluation evaluation)
        {
            Context.Evaluations.Remove(evaluation);
        }

        public Task<Evaluation> GetAnEvaluationWithItsVisitAsync(int visitId, int userId)
        {
            var x = 20;
            var n= Context.Evaluations.Include(evaluation => evaluation.Visit).FirstOrDefaultAsync(evaluation =>
                evaluation.VisitId == visitId && evaluation.EvaluatorId == userId);
            var z = 3;
            return n;

        }

        public async Task<bool> IsEvaluaterAsync(int userId, int visitId)
        {
            return await Context.Evaluations.AnyAsync(evaluation =>
                evaluation.VisitId == visitId && evaluation.EvaluatorId == userId);
        }

        public async Task<Evaluation> AddEvaluationAsync(Evaluation evaluation)
        {
            await Context.Evaluations.AddAsync(evaluation);
            return evaluation;
        }

        public async Task<bool> OppositeEvaluationAsync(int userId, int visitId)
        {
            var evaluation =await GetAnEvaluationAsync(userId, visitId);
            evaluation.Type =! evaluation.Type;
            return evaluation.Type;
        }

        public async Task<bool> IsUniqueEvaluatorAsync(int evaluatedId, int evaluatorId)
        {
            return await Context.Evaluations.Include(evaluation => evaluation.Visit)
                .AnyAsync(evaluation => evaluation.EvaluatorId ==evaluatorId&&evaluation.Visit.MedicalRepId==evaluatedId);
        }

        public EvaluationRepo(ApplicationContext context) : base(context)
        {
        }
    }
}