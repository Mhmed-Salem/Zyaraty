using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.EvaluationRepos;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.RequestResponseInteracting;
using Zyarat.Models.Services.InterActing;
using Zyarat.Responses.MedicalRepResponses;

namespace Zyarat.Models.Services.EvaluationsServices
{
    public class EvaluationService:IEvaluationService
    {
        private readonly IEvaluationRepo _repo;
        private readonly IUnitWork _unitWork;
        private readonly MedicalRepEvaluationsHandlers _medicalRepHandlers;
        private readonly VisitAssertion _visitAssertion;
        private readonly IVisitInteracting _interacting;
        private readonly IMapper _mapper;
        
        public EvaluationService(
            IEvaluationRepo repo,
            IUnitWork unitWork,
            MedicalRepEvaluationsHandlers medicalRepHandlers,
            VisitAssertion visitAssertion,
            IMapper mapper,
            IVisitInteracting interacting)
        {
            _repo = repo;
            _unitWork = unitWork;
            _medicalRepHandlers = medicalRepHandlers;
            _visitAssertion = visitAssertion;
            _mapper = mapper;
            _interacting = interacting;
        }

        public async Task<Response<Evaluation>> OppositeEvaluationAsync(int userId, int visitId)
        {
            try
            { 
                var evaluation = await _repo.GetAnEvaluationWithItsVisitAsync(userId, visitId);
                if (evaluation==null)
                {
                    return new Response<Evaluation>("evaluation is not found to Modify!");
                }
                if (_visitAssertion.AssertActivation(evaluation.Visit)&&!evaluation.Visit.Active)
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                await _repo.OppositeEvaluationAsync(userId, visitId);
                await _medicalRepHandlers.HandleEvaluationWithMedicalRepAsync(evaluation, evaluation.Visit,Interacting.Modify);
                await _unitWork.CommitAsync();
                return new Response<Evaluation>(evaluation);
            }
            catch (Exception e)
            {
                return new Response<Evaluation>($"Error:Can not Change Evaluation :{e.Message}");
            }
        }

   

        public async Task<Response<Evaluation>> AddEvaluationAsync(AddEvaluationDto contract)
        {
            try
            {
                if (await _repo.IsEvaluaterAsync(contract.EvaluatorId,contract.VisitId))
                {
                    return new Response<Evaluation>("You have already made Evaluation to this Visit!");
                }

                var visit = await _interacting.GetVisitAsync(contract.VisitId);
              
                if (!visit.Success||_visitAssertion.AssertActivation(visit.Source))
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                
                var ev =await _repo.AddEvaluationAsync(_mapper.Map<AddEvaluationDto, Evaluation>(contract));
                
                await _medicalRepHandlers.HandleEvaluationWithMedicalRepAsync(ev,visit.Source,Interacting.Add);
                await _repo.AddEvaluationAsync(ev);
                await _unitWork.CommitAsync();
                return  new Response<Evaluation>(ev);
            }
            catch (Exception e)
            {
                return  new Response<Evaluation>($"Error: {e.Message}");
            }
        }
        //can not  remove the evaluation except the evaluater himself
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
                
                if (!sVisitAsync.Success|| _visitAssertion.AssertActivation(sVisitAsync.Source))
                {
                    return  new Response<Evaluation>("Visit is not Active Or Deleted !");
                }
                
                _repo.DeleteEvaluation(ev);
                await _unitWork.CommitAsync();
                return new Response<Evaluation>(ev);
            }
            catch (Exception e)
            {
                return new Response<Evaluation>($"Error:{e.Message}");
            }
        }

        public async Task<Response<IEnumerable<Evaluation>>> GetEvaluatersAsync(int commentId)
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
    }
}