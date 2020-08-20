using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Models.DTO;
using Zyarat.Models.Factories;
using Zyarat.Models.Factories.MessageFactory;
using Zyarat.Models.Factories.NotificationFactory;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.NotificationRepo;
using Zyarat.Models.RequestResponseInteracting;
using Zyarat.Models.Services.InterActing;
using Zyarat.Models.Services.NotificationService;
using Zyarat.Responses.MedicalRepResponses;

namespace Zyarat.Models.Services.EvaluationsServices
{
    public class EvaluationService:NotificationService.NotificationService,IEvaluationService
    {
        private readonly IEvaluationRepo _repo;
        private readonly IUnitWork _unitWork;
        private readonly MedicalRepEvaluationsHandlers _medicalRepHandlers;
        private readonly VisitAssertion _visitAssertion;
        private readonly IVisitInteracting _interacting;
        private readonly IMapper _mapper;
        private readonly INotificationTypeRepo _notificationTypeRepo; 
        private readonly EvaluationEventBuilder _eventBuilder;
        
        //tested
        public async  Task<Response<Evaluation>> GetEvaluation(int evaluationId)
        {
            try
            {
                var evaluation =await  _repo.GetEvaluationById(evaluationId);
                return evaluation==null ? new Response<Evaluation>("Not found!") : new Response<Evaluation>(evaluation);
            }
            catch (Exception e)
            {
                return new Response<Evaluation>($"ERROR :{e.Message}");
            }
        }

        public async Task<Response<Evaluation>> OppositeEvaluationAsync(int userId, int visitId)
        {
            try
            { 
                var evaluation = await _repo.GetAnEvaluationWithItsVisitAsync(visitId,userId);
                if (evaluation==null)
                {
                    return new Response<Evaluation>("evaluation is not found to Modify!");
                }
                if (!_visitAssertion.AssertActivation(evaluation.Visit))
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                await _repo.OppositeEvaluationAsync(userId:userId, visitId:visitId);
                await _medicalRepHandlers.HandleEvaluationWithMedicalRepAsync(evaluation, evaluation.Visit,Interacting.Modify);
                await _unitWork.CommitAsync();
                //handle Notification
                var not = await GetEvent(evaluation.Id, (int)NotificationTypesEnum.Evaluation);
                if (!not.Success)
                {
                    return new Response<Evaluation>("Event is opposited successfully,but Notification is not !");
                }
                var opposite=new EvaluationOpposite(not.Source);
                opposite.Opposite();
                //end of handling Notification
                await _unitWork.CommitAsync();
                return new Response<Evaluation>(evaluation);
            }
            catch (Exception e)
            {
                return new Response<Evaluation>($"Error:Can not Change Evaluation :{e.Message}");
            }
        }

   
        //tested

        public async Task<Response<Evaluation>> AddEvaluationAsync(AddEvaluationDto contract)
        {
            try
            {
                if (await _repo.IsEvaluatorAsync(contract.EvaluatorId,contract.VisitId))
                {
                    return new Response<Evaluation>("You have already made Evaluation to this Visit!");
                }

                var visit = await _interacting.GetVisitAsync(contract.VisitId);
                
                if (!visit.Success||!_visitAssertion.AssertActivation(visit.Source))
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                
                var ev =await _repo.AddEvaluationAsync(_mapper.Map<AddEvaluationDto, Evaluation>(contract));
                await _medicalRepHandlers.HandleEvaluationWithMedicalRepAsync(ev,visit.Source,Interacting.Add);
                await _unitWork.CommitAsync();
                //adding Notification
                var allEval =await _repo.GetEvaluationWithItsVisitAndDoctorAndRepByIdAsync(ev.Id);
                _eventBuilder.Init(allEval,allEval.Evaluator,allEval.Visit,allEval.Visit.Doctor);
                var notification=await _eventBuilder.Build();
                await AddEventNotificationAsync(notification);
                //end of adding Notification
                return  new Response<Evaluation>(ev);
            }
            catch (Exception e)
            {
                return  new Response<Evaluation>($"Error: {e.Message}");
            }
        }
        //can not  remove the evaluation except the evaluator himself
        public async Task<Response<Evaluation>> RemoveEvaluationAsync(int visitId, int userId)
        {
            try
            {
                var ev =  await _repo.GetAnEvaluationAsync(visitId, userId);
                if (ev==null)
                {
                    return  new Response<Evaluation>("the evaluation is not found !");
                }

                var sVisitAsync = await _interacting.GetVisitAsync(visitId);
                if (!sVisitAsync.Success||!_visitAssertion.AssertActivation(sVisitAsync.Source) )
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                
                _repo.DeleteEvaluation(ev);
                await _medicalRepHandlers.HandleEvaluationWithMedicalRepAsync(ev,sVisitAsync.Source,Interacting.Delete);
                await _unitWork.CommitAsync();
                //Handle Notification
                await DeleteEvent(ev.Id, (int) NotificationTypesEnum.Evaluation);
                //end of handling Notification
                return new Response<Evaluation>(ev);
            }
            catch (Exception e)
            {
                return new Response<Evaluation>($"Error:{e.Message}");
            }
        }

        public async Task<Response<IEnumerable<Evaluation>>> GetEvaluatorsAsync(int commentId)
        {
            try
            {
                var evaluators =await _repo.GetEvaluationsAsync(commentId);
                return new Response<IEnumerable<Evaluation>>(evaluators);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<Evaluation>>($"Error:{e.Message}");
            }
        }

        public EvaluationService(
            IUnitWork unitWork,
            INotificationRepo repo,
            IGlobalMessageFactory globalMessageFactory,
            IMessageFactory messageFactory,
            IEvaluationRepo repo1,
            MedicalRepEvaluationsHandlers medicalRepHandlers,
            VisitAssertion visitAssertion,
            IVisitInteracting interacting,
            IMapper mapper, 
            INotificationTypeRepo notificationTypeRepo
            ) : base(unitWork, repo, globalMessageFactory, messageFactory)
        {
            _unitWork = unitWork;
            _repo = repo1;
            _medicalRepHandlers = medicalRepHandlers;
            _visitAssertion = visitAssertion;
            _interacting = interacting;
            _mapper = mapper;
            _notificationTypeRepo = notificationTypeRepo;
            _eventBuilder=new EvaluationEventBuilder(_notificationTypeRepo);
        }
    }
}