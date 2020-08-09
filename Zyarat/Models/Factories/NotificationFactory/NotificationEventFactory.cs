using System;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Helpers;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.NotificationRepo;

namespace Zyarat.Models.Factories.NotificationFactory
{
    public class NotificationEventFactory : INotificationEventFactory
    {
        private readonly IEvaluationRepo _evaluationRepo;
        private readonly INotificationTypeRepo _notificationTypeRepo;
        private readonly IMedicalRepRepo _medicalRepRepo;

        public NotificationEventFactory(IEvaluationRepo evaluationRepo, INotificationTypeRepo notificationTypeRepo)
        {
            _evaluationRepo = evaluationRepo;
            _notificationTypeRepo = notificationTypeRepo;
        }

        public async Task<EventNotification> CreateAsync(NotificationTypesEnum typesEnum, int dataId, int eventOwner)
        {
            if (typesEnum == NotificationTypesEnum.Evaluation)
            {
                var ev = await _evaluationRepo.GetEvaluationWithItsVisitAndDoctorAndRepByIdAsync(dataId);
                if (ev == null) throw new Exception("Evaluation Is Not Exist");
                if (ev.EvaluatorId != eventOwner) throw new Exception("you can not add this Notification");
     
                var template = (await _notificationTypeRepo.Get(typesEnum)).Template;
                
                var v= new EventNotification
                {
                    DateTime = DateTime.Now,
                    NotificationTypeId = (int) NotificationTypesEnum.Evaluation,
                    MedicalRepId = ev.Visit.MedicalRepId,
                    DataId = ev.Id,
                    Message = template.ReplaceHolders("{}",new []
                    {
                        string.Concat(ev.Evaluator.FName, " ", ev.Evaluator.LName),
                        ev.Type?"Like":"DisLike",
                        string.Concat(ev.Visit.Doctor.FName, " ", ev.Visit.Doctor.LName),
                        ev.Visit.Content
                    })
                };
                
                return v;
            }

            throw new Exception("Type is not Valid !");

        }
    }
}