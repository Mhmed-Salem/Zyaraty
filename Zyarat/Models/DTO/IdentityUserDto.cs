using System.ComponentModel.DataAnnotations;

namespace Zyarat.Models.DTO
{
    public class IdentityUserDto
    {
        [EmailAddress,Required]
        public string Email { set; get; }
        [MaxLength(11)]
        public string Phone { set; get; }
    }
}