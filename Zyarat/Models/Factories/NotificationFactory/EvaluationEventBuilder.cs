using System;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Helpers;
using Zyarat.Models.Repositories.NotificationRepo;

namespace Zyarat.Models.Factories.NotificationFactory
{
    public class EvaluationEventBuilder : EventBuilder
    {
        private Evaluation _evaluation;
        private MedicalRep _evaluator;
        private Visit _visit;
        private Doctor _doctor;

        public void Init(Evaluation evaluation, MedicalRep evaluator, Visit visit, Doctor doctor)
        {
            _doctor = doctor;
            _visit = visit;
            _evaluation = evaluation;
            _evaluator = evaluator;
        }
        public override async Task<EventNotification> Build()
        {
            var template = (await _notificationTypeRepo.Get(NotificationTypesEnum.Evaluation)).Template;
            var v = new EventNotification
            {
                DateTime = DateTime.Now,
                NotificationTypeId = (int) NotificationTypesEnum.Evaluation,
                MedicalRepId = _visit.MedicalRepId,
                DataId = _evaluation.Id,
                Message = template.ReplaceHolders("{}", new[]
                {
                    string.Concat(_evaluator.FName, " ", _evaluation.Evaluator.LName),
                    _evaluation.Type ? "Like" : "DisLike",
                    string.Concat(_doctor.FName, " ", _doctor.LName),
                    _visit.Content
                })
            };
            return v;
        }

        public EvaluationEventBuilder(INotificationTypeRepo notificationTypeRepo) : base(notificationTypeRepo)
        {
            
        }
    }
}