﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Zyarat.Contract.MedicalRepsContracts;
using Zyarat.Data;
using Zyarat.Models.DTO;
using Zyarat.Resources;
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
            CreateMap<VisitBlocking, VisitBlockingDto>();
            CreateMap<VisitReport, VisitReportDto>();
            CreateMap<UpdateRepResource, MedicalRep>();
            CreateMap<AddMedicalRepResourcesRequest, MedicalRep>();
            CreateMap<IdentityUserDto, IdentityUser>();
            CreateMap<MedicalRep, GetMedicalRepResponse>();

            
        }
    }
}