﻿using System;

namespace Zyarat.Models.DTO
{
    public class GetVisitByCityDto
    {
        public int Id { set; get; }
        public bool Type { set; get; }
        public MedicalRepForVisitDto Rep { set; get; }
        public string Content { set; get; }
        public int Likes { set; get; }
        public int DisLikes { set; get; }
        public bool IsLiker { set; get; }
        public bool IsDisLiker { set; get; }
        public DoctorDto DoctorDto { set; get; }
        public DateTime DateTime { set; get; }
        public bool IsActive { set; get; }


    }
}