using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing.Matching;

namespace Zyarat.Contract.Doctor
{
    public class AddDoctorDto
    {
        [Required,MaxLength(10)]
        public string FName { set; get; }
        [Required,MaxLength(10)]
        public string LName { set; get; }
        [Required]
        public int CityId { set; get; }
        [Required]
        public int AdderMedicalRepId { set; get; }
        [Required]
        public int MedicalSpecializedId { set; get; }
    }
}