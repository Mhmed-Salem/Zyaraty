using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.EvaluationsServices
{
    public interface IEvaluationService
    {
        Task<Response<Evaluation>> GetEvaluation(int evaluationId);
        Task<Response<Evaluation>> OppositeEvaluationAsync(int userName,int visitId);
        Task<Response<Evaluation>> AddEvaluationAsync(AddEvaluationDto contract);
        Task<Response<Evaluation>> RemoveEvaluationAsync(int visitId, int userId);
        Task<Response<IEnumerable<Evaluation>>> GetEvaluatorsAsync(int commentId);
    }
}