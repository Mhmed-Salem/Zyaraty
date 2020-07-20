using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Handlers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Models.Repositories.VisitsRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.IVisitService.VisitsServices
{
    public class VisitService:IVisitService
    {
        private readonly IUnitWork _unitWork;
        private readonly IVisitsRepo _repo;
        private readonly MedicalRepHandlers _medicalRepHandler;
        private readonly DeletingVisitsHandler _deletingVisitsHandler;
        private readonly IMedicalRepRepo _medicalRepRepo;
        /// <summary>
        /// limits after that the visit will not be displayed in doctors Visits
        /// </summary>
        private const int LimitedDaysToDisplayInDoctor = 7;

        /// <summary>
        /// limits after that the visit will not be displayed in latest visits for a city
        /// </summary>
        private const int LimitedDaysToDisplayInLatest = 7;
        
        public VisitService(IUnitWork unitWork, IVisitsRepo repo, IMapper mapper, MedicalRepHandlers handler, DeletingVisitsHandler deletingVisitsHandler, IMedicalRepRepo medicalRepRepo)
        {
            //_unitWork = unitWork;
            _unitWork = unitWork;
            _repo = repo;
            _medicalRepHandler = handler;
            _deletingVisitsHandler = deletingVisitsHandler;
            _medicalRepRepo = medicalRepRepo;
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
                await _medicalRepHandler.HandleAddingVisit(visit);
                await _unitWork.CommitAsync();
                int x = 22;
                return new Response<Visit>(visit);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOR");
                return new Response<Visit>($"Can not Add the visit: {e.Message}");
            }
        }

       protected async Task<Response<Visit>> RemoveVisit(int visitId)
        {
            try
            {
                var visit = await _repo.GetVisit(visitId);
                _repo.DeleteVisit(visit);
                await _medicalRepHandler.RemoveVisitAsync(visit);
                await _unitWork.CommitAsync();
                return new Response<Visit>(visit);
            }
            catch (Exception e)
            {
                return new Response<Visit>("Can not delete the visit");
            }
        }

        public async Task<Response<IEnumerable<GetVisitByDoctorDto>>> GetVisitByDoctor(int doctorId, int userId)
        {
            try
            {
                var visits = _repo.GetVisitOfDoctorAsync(doctorId, userId); 
                var validVisits=_deletingVisitsHandler.HandleDeleting(visits);
                await _unitWork.CommitAsync();
                var rt= validVisits.Where(visit => visit.Active && visit.DateTime.AddDays(LimitedDaysToDisplayInDoctor)>DateTime.Now)
                    .Select(visit => new GetVisitByDoctorDto
                    {
                        DateTime = visit.DateTime,
                        Content = visit.Content,
                        Id = visit.Id,
                        Likes = visit.Evaluation.Count(evaluation => evaluation.Type),
                        DisLikes = visit.Evaluation.Count(evaluation => !evaluation.Type),
                        UserName = visit.MedicalRep.IdentityUser.UserName,
                        IsLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluaterId==visit.MedicalRepId &&evaluation.Type),
                        IsDisLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluaterId==visit.MedicalRepId &&!evaluation.Type)
                    }).OrderByDescending(dto => dto.DateTime).ThenByDescending(dto=>dto.Likes);
                return new Response<IEnumerable<GetVisitByDoctorDto>>(rt);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetVisitByDoctorDto>>($"Error: {e.Message}");
            }
        }

        public async Task<Response<IEnumerable<GetVisitByCityDto>>> GetVisitByCity(int cityId, int userId)
        {
            try
            {
                var visits = _repo.GetLatestInCityAsync(cityId, userId); 
                var validVisits=_deletingVisitsHandler.HandleDeleting(visits);
                var rt= validVisits.Where(visit => visit.Active && visit.DateTime.AddDays(LimitedDaysToDisplayInLatest)>DateTime.Now)
                    .Select(visit => new GetVisitByCityDto()
                    {
                        DateTime = visit.DateTime,
                        Content = visit.Content,
                        Id = visit.Id,
                        Likes = visit.Evaluation.Count(evaluation => evaluation.Type),
                        DisLikes = visit.Evaluation.Count(evaluation => !evaluation.Type),
                        UserName = visit.MedicalRep.IdentityUser.UserName,
                        DoctorDto = new DoctorDto
                        {
                            FName = visit.Doctor.FName,
                            LName = visit.Doctor.LName,
                            Id = visit.Doctor.Id,
                        },
                        IsLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluaterId==visit.MedicalRepId &&evaluation.Type),
                        IsDisLiker = visit.Evaluation.Any(evaluation => evaluation.EvaluaterId==visit.MedicalRepId &&!evaluation.Type)
                    })
                    .OrderByDescending(dto => dto.DateTime).ThenByDescending(dto=>dto.Likes);

                await _unitWork.CommitAsync();
                return new Response<IEnumerable<GetVisitByCityDto>>(rt);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<GetVisitByCityDto>>($"Error: {e.Message}");
            }        }
    }
}