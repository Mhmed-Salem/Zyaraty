using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.VisitsRepo;

namespace Zyarat.Handlers
{
    public class MedicalRepEvaluationsHandlers
    {

        private readonly IMedicalRepRepo _medicalRepRepo;
        private readonly IEvaluationRepo _evaluationRepo;

        private int _allowablePercentageOfDisLikeToLike = 80;

        public MedicalRepEvaluationsHandlers(IMedicalRepRepo medicalRepRepo,  IEvaluationRepo evaluationRepo)
        {
            _medicalRepRepo = medicalRepRepo;
            _evaluationRepo = evaluationRepo;
        }

        public void SetAllowablePercentageOfDisLikeToLike(int percent)
        {
            _allowablePercentageOfDisLikeToLike = percent;
        }

        public bool RebExists { private set; get; }

        public async Task HandleEvaluationWithMedicalRepAsync(Evaluation ev,Visit visit,Interacting interacting)
        {

            var rep = await _medicalRepRepo.GetUserAsync(ev.Visit.MedicalRepId);
            if (ev.Type && interacting == Interacting.Add || interacting == Interacting.Modify && ev.Type)
            {
                ++rep.LikeCount;
            }

            if (!ev.Type && interacting == Interacting.Add || interacting == Interacting.Modify && !ev.Type)
            {
                ++rep.DisLikeCount;
            }

            if (ev.Type && interacting == Interacting.Delete || interacting == Interacting.Modify && !ev.Type)
            {
                --rep.LikeCount;
            }
            else if (!ev.Type && interacting == Interacting.Delete || interacting == Interacting.Modify && ev.Type)

            {
                --rep.DisLikeCount;
            }

            DeleteIfDeserve(visit.MedicalRep);

            if (await _evaluationRepo.IsUniqueEvaluatorAsync(visit.MedicalRepId,ev.EvaluatorId)&& ev.Type)
            {
                if (interacting == Interacting.Add)
                {
                    ++rep.UniqueUsers;
                }
                else --rep.UniqueUsers;
            }

        }
    
        private  void DeleteIfDeserve(MedicalRep rep)
        {
            var div = rep.LikeCount + rep.DisLikeCount;
            if (div==0)
            {
                return;
            }
            if (rep.DisLikeCount / div * 100 > _allowablePercentageOfDisLikeToLike)
            {
                _medicalRepRepo.DeleteUser(rep);
                RebExists = false;
            }
            else RebExists = true;
        }
    }
}