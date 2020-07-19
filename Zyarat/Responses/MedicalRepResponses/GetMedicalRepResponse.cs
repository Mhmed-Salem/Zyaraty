using System.ComponentModel.DataAnnotations.Schema;
using Zyarat.Data;
using Zyarat.Models.DTO;

namespace Zyarat.Responses.MedicalRepResponses
{
    public class GetMedicalRepResponse
    {
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string ProfileUrl { set; get; }
        public string WorkedOnCompany { set; get; }
        [NotMapped]
        public string Email { set; get; }
        [NotMapped]
        public string Phone { set; get; }
        [NotMapped]
        public string UserName { set; get; }
        public CityDto City { set; get; }
        public MedicalRepPositionDto MedicalRepPosition { set; get; }
        public int VisitsCount { set; get; }
        public int LikeCount { set; get;}
        public int DisLikeCount { set; get; }
        public int UniqueUsers { set; get; }
       

    }
}