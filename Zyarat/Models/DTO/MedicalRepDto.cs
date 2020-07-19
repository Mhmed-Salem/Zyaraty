using Microsoft.AspNetCore.Identity;

namespace Zyarat.Models.DTO
{
    public class MedicalRepDto
    {
        public int Id { set; get; }
        public IdentityUserDto IdentityUser { set; get; }
        public string WorkedOnCompany { set; get; }
        public CityDto City { set; get; }
        public int VisitsCount { set; get; }
        public int LikeCount { set; get;}
        public int DisLikeCount { set; get; }
        public int UniqueUsers { set; get; }
        public MedicalRepPositionDto MedicalRepPosition { set; get; }
        public VisitBlockingDto VisitBlocking { set; get; }


    }
}