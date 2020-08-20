using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;

namespace Zyarat.Models.Repositories.EvaluationRepos
{
    public interface IEvaluationRepo
    {
        Task<Evaluation> GetEvaluationById(int evaluationId);
        Task<IEnumerable<Evaluation>> GetEvaluationsAsync(int visitId);
        Task<Evaluation> GetEvaluationWithItsVisitAndDoctorAndRepByIdAsync(int evaluationId);
        Task<Evaluation> GetAnEvaluationAsync(int visitId,int userId);
        void DeleteEvaluation(Evaluation evaluation);
        Task<Evaluation> GetAnEvaluationWithItsVisitAsync(int visitId, int userId);
        Task<Evaluation> GetAnEvaluationWithItsVisitAsync(int evaluationId);

        Task<bool> IsEvaluatorAsync(int userId,int visitId);
        Task<Evaluation> AddEvaluationAsync(Evaluation evaluation);
        Task<bool> OppositeEvaluationAsync(int userId,int visitId);
        Task<bool> IsUniqueEvaluatorAsync(int evaluatedIdId, int evaluaterId);
        Task<int> TimesTheUserEvaluateAnotherUserVisitsAsLikeWithNoTracking(int evaluatedId, int evaluatorId);
    }
}