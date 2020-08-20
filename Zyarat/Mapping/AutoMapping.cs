using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Zyarat.Contract.Doctor;
using Zyarat.Contract.MedicalRepsContracts;
using Zyarat.Contract.VisitsContracts;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Resources;
using Zyarat.Responses;
using Zyarat.Responses.MedicalRepResponses;

namespace Zyarat.Mapping
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<City, CityDto>();
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Evaluation, EvaluationDto>();
            CreateMap<Government, GovernmentDto>();
            CreateMap<MedicalRep, MedicalRepDto>();
            CreateMap<MedicalRepPosition, MedicalRepPositionDto>();
            CreateMap<MedicalSpecialized, MedicalSpecializationDto>();
            CreateMap<Visit, VisitDto>();
            CreateMap<VisitReport, VisitReportDto>();
            CreateMap<UpdateRepResource, MedicalRep>();
            CreateMap<AddMedicalRepResourcesRequest, MedicalRep>();
            CreateMap<IdentityUserDto, IdentityUser>();
            CreateMap<MedicalRep, GetMedicalRepResponse>();
            CreateMap<AddVisitContract, Visit>();
            CreateMap<Visit, AddVisitContract>();
            CreateMap<AddEvaluationDto, Evaluation>();
            CreateMap<Evaluation, AddEvaluationDto>();
            CreateMap<Evaluation, AddEvaluationResponse>();
            CreateMap<Evaluation, GetEvaluationsResponse>();
            CreateMap<MedicalRep, GetUnActiveUsersResponse>();
            CreateMap<CompetitionDto, Competition>();
            CreateMap<Competition, CompetitionDto>();
            CreateMap<Visit, AddVisitDto>();
            CreateMap<Winner, WinnerDto>();
            CreateMap<WinnerDto, Winner>();
            CreateMap<AddDoctorDto, Doctor>();
            CreateMap<Doctor, DoctorResponse>();
            CreateMap<EventNotificationDto, EventNotification>();
            CreateMap<EventNotification, EventNotificationDto>();
            CreateMap<NotificationType, NotificationTypeDto>();
            CreateMap<NotificationTypeDto, NotificationType>();
        }
    }
}