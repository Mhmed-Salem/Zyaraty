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
        public async Task<Evaluation> GetEvaluationById(int evaluationId)
        {
            return await Context.Evaluations.FindAsync(evaluationId);
        }

        public async Task<IEnumerable<Evaluation>> GetEvaluationsAsync(int visitId)
        {
            return await Context.Evaluations.Include(evaluation => evaluation.Evaluator)
                .Where(evaluation => evaluation.VisitId==visitId).ToListAsync();
        }

        public async  Task<Evaluation> GetEvaluationWithItsVisitAndDoctorAndRepByIdAsync(int evaluationId)
        {
            return await Context.Evaluations.Where(evaluation => evaluation.Id == evaluationId)
                .Include(evaluation => evaluation.Visit)
                .Include(evaluation => evaluation.Evaluator)
                .Include(evaluation => evaluation.Visit.Doctor)
                .FirstOrDefaultAsync();
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

        public async Task<Evaluation> GetAnEvaluationWithItsVisitAsync(int visitId, int userId)
        {
            return await Context.Evaluations.Include(evaluation => evaluation.Visit).FirstOrDefaultAsync(evaluation =>
                evaluation.VisitId == visitId && evaluation.EvaluatorId == userId);

        }

        public async  Task<Evaluation> GetAnEvaluationWithItsVisitAsync(int evaluationId)
        {
            return await Context.Evaluations.Include(evaluation => evaluation.Visit)
                .FirstOrDefaultAsync(evaluation => evaluation.Id == evaluationId);
        }

        public async Task<bool> IsEvaluatorAsync(int userId, int visitId)
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
            var evaluation =await GetAnEvaluationAsync(visitId, userId);
            evaluation.Type =! evaluation.Type;
            return evaluation.Type;
        }

        public async Task<bool> IsUniqueEvaluatorAsync(int evaluatedId, int evaluatorId)
        {
            return await Context.Evaluations.Include(evaluation => evaluation.Visit)
                .AsNoTracking()
                .AnyAsync(evaluation => evaluation.EvaluatorId ==evaluatorId
                                        &&evaluation.Visit.MedicalRepId==evaluatedId);
        }

        public async Task<int> TimesTheUserEvaluateAnotherUserVisitsAsLikeWithNoTracking(int evaluatedId, int evaluatorId)
        {
            return await Context.Evaluations.AsNoTracking()
                .Include(evaluation => evaluation.Visit)
                .CountAsync(evaluation => evaluation.EvaluatorId == evaluatorId
                                          && evaluation.Visit.MedicalRepId == evaluatedId
                                          &&evaluation.Type);
        }

        public EvaluationRepo(ApplicationContext context) : base(context)
        {
        }
    }
}