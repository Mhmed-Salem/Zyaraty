using System;

namespace Zyarat.Models.DTO
{
    public class GetVisitByDoctorDto
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string Content { set; get; }
        public int Likes { set; get; }
        public int DisLikes { set; get; }
        public bool IsLiker { set; get; }
        public bool IsDisLiker { set; get; }
        public bool IsActive { set; get; }
        public DateTime DateTime { set; get; }

    }
}