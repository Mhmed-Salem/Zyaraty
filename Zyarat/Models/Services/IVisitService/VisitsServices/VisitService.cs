using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.VisitsRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.IVisitService.VisitsServices
{
    public class VisitService:IVisitService
    {
        private readonly IUnitWork _unitWork;
        private readonly IVisitsRepo _repo;
        private readonly MedicalRepVisitsHandlers _medicalRepHandler;
        /// <summary>
        /// limits after that the visit will not be displayed in doctors Visits
        /// </summary>
        private const int LimitedDaysToDisplayInDoctor = 7;
        /// <summary>
        /// limits after that the visit will not be displayed in latest visits for a city
        /// </summary>
        private const int LimitedDaysToDisplayInLatest = 7;
        /// <summary>
        /// limits after that the Medical Reps can not Interact with the visit(no likes or dislikes)
        /// </summary>
        private const int LimitsOfVisitActivationHours = 3;

  
        
        public VisitService(IUnitWork unitWork,
            IVisitsRepo repo,
            IMapper mapper,
            MedicalRepVisitsHandlers handler)
        {
            //_unitWork = unitWork;
            _unitWork = unitWork;
            _repo = repo;
            _medicalRepHandler = handler;
        }

        public async  Task<Response<Visit>> GetVisit(int visitId)
        {
            try
            {
                var visit = await _repo.GetVisit(visitId);
                return visit == null ? new Response<Visit>("Error :Not Found") : new Response<Visit>(visit);
            }
            catch (Exception e)
            {
              return new Response<Visit>($"Error :{e.Message}");
            }
        }

        public async Task<Response<Visit>> AddVisit(AddVisitContract contract)
        {
            try
            {
                var visit = new Visit
                {
                    Active = true,
                    DateTime = DateTime.Now,
                    Content = contract.Content,
                    DoctorId = contract.DoctorId,
                    MedicalRepId = contract.MedicalRepId,
                    Type = contract.Typ
                };
                await _repo.AddVisitAsync(visit);
                await _medicalRepHandler.HandleAddingVisitAsync(visit);
                await _unitWork.CommitAsync();
                return new Response<Visit>(visit);
                
            }
            catch (Exception e)
            {
                return new Response<Visit>($"Can not Add the visit: {e.Message}");
            }
        }

       protected async Task<Response<Visit>> RemoveVisit(int visitId)
        {
            try
            {
                var visit = await _repo.GetVisit(visitId);
                _repo.DeleteVisit(visit);
                await _medicalRepHandler.HandleRemovingVisitAsync(visit);
                await _unitWork.CommitAsync();
                return new Response<Visit>(visit);
            }
            catch (Exception e)
            {
                return new Response<Visit>($"Can not delete the visit{e.Message}");
            }
        }
       
       public async Task<Response<IEnumerable<GetVisitByDoctorDto>>> GetVisitByDoctor([FromQuery]int doctorId,[FromQuery] int userId)
        {
            try
            {
                var visits = _repo.GetVisitOfDoctorAsync(doctorId, userId); 
                var validVisits=_medicalRepHandler.HandleDeleting(visits);
                var rt= validVisits.Where(visit => visit.Active && visit.DateTime.AddDays(LimitedDaysToDisplayInDoctor)>DateTime.Now)
                    .Select(visit => new GetVisitByDoctorDto
                    {
                        DateTime = visit.DateTime,
                        Type = visit.Active,
                        Id = visit.Id,
                        Rep = new MedicalRepForVisitDto
                        {
                            Id = visit.Id,
                            UserName = visit.MedicalRep.IdentityUser.UserName,
                            FName = visit.MedicalRep.FName,
                            LName = visit.MedicalRep.LName,
                            ProfileUrl = visit.MedicalRep.ProfileUrl
                        },
                        Content = visit.Content,
                        IsActive = visit.DateTime.AddHours(LimitsOfVisitActivationHours)>DateTime.Now,
                        Likes = visit.Evaluation.Count(evaluation => evaluation.Type),
                        DisLikes = visit.Evaluation.Count(evaluation => !evaluation.Type),
                        IsLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluatorId==visit.MedicalRepId &&evaluation.Type),
                        IsDisLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluatorId==visit.MedicalRepId &&!evaluation.Type),
                    }).OrderByDescending(dto => dto.DateTime).ThenByDescending(dto=>dto.Likes);
                await _unitWork.CommitAsync();
                return new Response<IEnumerable<GetVisitByDoctorDto>>(rt);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetVisitByDoctorDto>>($"Error: {e.Message}");
            }
        }

        public async Task<Response<Visit>> GetVisitAsync(int visitId)
        {
            var visit =await _repo.GetVisit(visitId);
            return visit==null ? new Response<Visit>("Visit Not found !") : new Response<Visit>(visit);
        }

        public  bool IsActiveComment(Visit visit)
        {
            return visit.DateTime.AddHours(LimitsOfVisitActivationHours) > DateTime.Now
                   && visit.Active;
        }

        public async Task<Response<Visit>> GetVisitReportsAsync(int visitId)
        {
            try
            {
                var vis = await _repo.GetVisitWithItsReportsAsync(visitId);
                return new Response<Visit>(vis);
            }
            catch (Exception e)
            {
                return new Response<Visit>($"Error: {e.Message}");
            }
        }

        public async Task<Response<IEnumerable<GetVisitByCityDto>>> GetVisitByCity(int cityId, int userId)
        {
            try
            {
                var visits = _repo.GetLatestInCityAsync(cityId, userId); 
                var validVisits=_medicalRepHandler.HandleDeleting(visits);
                var rt= validVisits.Where(visit => visit.Active && visit.DateTime.AddDays(LimitedDaysToDisplayInLatest)>DateTime.Now)
                    .Select(visit => new GetVisitByCityDto
                    {
                        DateTime = visit.DateTime,
                        Type = visit.Type,
                        Rep = new MedicalRepForVisitDto
                        {
                            Id = visit.MedicalRepId,
                            FName = visit.MedicalRep.FName,
                            LName = visit.MedicalRep.LName,
                            UserName = visit.MedicalRep.IdentityUser.UserName,
                            ProfileUrl = visit.MedicalRep.ProfileUrl
                        },
                        Content = visit.Content,
                        Id = visit.Id,
                        IsActive = visit.DateTime.AddHours(LimitsOfVisitActivationHours)>DateTime.Now,
                        Likes = visit.Evaluation.Count(evaluation => evaluation.Type),
                        DisLikes = visit.Evaluation.Count(evaluation => !evaluation.Type),
                        DoctorDto = new DoctorDto
                        {
                            FName = visit.Doctor.FName,
                            LName = visit.Doctor.LName,
                            Id = visit.Doctor.Id,
                        },
                        IsLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluatorId==visit.MedicalRepId &&evaluation.Type),
                        IsDisLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluatorId==visit.MedicalRepId &&!evaluation.Type)
                    })
                    .OrderByDescending(dto => dto.DateTime).ThenByDescending(dto=>dto.Likes);

                await _unitWork.CommitAsync();
                return new Response<IEnumerable<GetVisitByCityDto>>(rt);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetVisitByCityDto>>($"Error: {e.Message}");
            }
        }
    }
}