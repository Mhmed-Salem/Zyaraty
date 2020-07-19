using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Zyarat.Models.DTO;

namespace Zyarat.Resources
{
    public class AddMedicalRepResourcesRequest
    {
        [MaxLength(20),Required]
        public string FName { set; get; }
        [MaxLength(20),Required]
        public string LName { set; get; }
        [Required,MaxLength(30)]
        public string Password { set; get; }
        [EmailAddress,Required]
        public string Email { set; get; } 
        [Phone,Required]
        public string Phone { set; get; }
        public int CityId { set; get; }
        public int MedicalRepPositionId { set; get; }
        public string WorkedOnCompany { set; get; }
        [NotMapped]
        public IFormFile Image { set; get; }
    }
}