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

        //tested
        public async Task HandleEvaluationWithMedicalRepAsync(Evaluation ev,Visit visit,Interacting interacting)
        {

            var rep = await _medicalRepRepo.GetUserAsync(ev.Visit.MedicalRepId);
            //tested
            if (ev.Type && interacting == Interacting.Add || interacting == Interacting.Modify && ev.Type)
            {
                ++rep.LikeCount;
            }
            //tested
            if (!ev.Type && interacting == Interacting.Add || interacting == Interacting.Modify && !ev.Type)
            {
                ++rep.DisLikeCount;
            }
            //tested
            if (ev.Type && interacting == Interacting.Delete || interacting == Interacting.Modify && !ev.Type)
            {
                --rep.LikeCount;
            }
            //tested
            else if (!ev.Type && interacting == Interacting.Delete || interacting == Interacting.Modify && ev.Type)
            {
                --rep.DisLikeCount;
            }

            UnActiveRepIfDeserve(visit.MedicalRep);
            
            /**
             * Handle UniqueUsers Count 
             */
            
            var countOfEvaluation =await 
                _evaluationRepo.TimesTheUserEvaluateAnotherUserVisitsAsLikeWithNoTracking(
                    visit.MedicalRepId, ev.EvaluatorId);
            if ((countOfEvaluation == 0 && (ev.Type && interacting == Interacting.Add)) ||
                (countOfEvaluation == 0 && (ev.Type && interacting == Interacting.Modify)))
                rep.UniqueUsers++;
            else if ((countOfEvaluation == 1 && (ev.Type && interacting == Interacting.Delete)) ||
                     (countOfEvaluation == 1 && (!ev.Type && interacting == Interacting.Modify)))
                rep.UniqueUsers--;
        }

   
    
        private  void UnActiveRepIfDeserve(MedicalRep rep)
        {
            var div = rep.LikeCount + rep.DisLikeCount;
            if (div==0)
            {
                return;
            }

            if ( rep.DisLikeCount / div * 100>= _allowablePercentageOfDisLikeToLike)
            {
                _medicalRepRepo.UnActive(rep);
                RebExists = false;
            }
            else RebExists = true;
        }
    }
}