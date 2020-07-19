using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Zyarat.Data
{
    public class RefreshingToken
    {
        public int Id { set; get; }
        public string RefreshToken { set; get; }
        public string IP { set; get; }
        public DateTime CreationDate { set; get; }
        
        public IdentityUser MyIdentityUser { set; get; }
        [ForeignKey(nameof(MyIdentityUser))]
        public string UserId { set; get; }
    }
}