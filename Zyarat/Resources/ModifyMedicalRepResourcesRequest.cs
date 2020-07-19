using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Zyarat.Models.DTO;

namespace Zyarat.Resources
{
    public class ModifyMedicalRepResourcesRequest
    {
        [MaxLength(20),Required]
        public string FName { set; get; }
        [MaxLength(20),Required]
        public string LName { set; get; }
        [NotMapped]
        public string Phone { set; get; }
        [Required]
        public int CityId { set; get; }
        public int MedicalRepPositionId { set; get; }
        public string WorkedOnCompany { set; get; }
        [NotMapped]
        public IFormFile Image { set; get; }
    }
}