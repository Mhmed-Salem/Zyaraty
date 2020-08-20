using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Factories;
using Zyarat.Models.Repositories.EvaluationRepos;

namespace Zyarat.Models.Services.NotificationService
{
    public class EventRoute:IEventRoute
    {
        private readonly EvaluationRepo _evaluationRepo;

        public EventRoute(EvaluationRepo evaluationRepo)
        {
            _evaluationRepo = evaluationRepo;
        }


        public async  Task<object> Route(NotificationTypesEnum typesEnum, int dataId)
        {
            switch (typesEnum)
            {
                case NotificationTypesEnum.Evaluation: return await _evaluationRepo.GetAnEvaluationWithItsVisitAsync(dataId);
                default: return null;
            }
        }
    }
}