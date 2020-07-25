using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Zyarat.Data;
using Zyarat.Resources;

namespace Zyarat.Models.Services.IdentityServices
{
    public interface IIdentityUser
    {
        Task<RegisterServiceResult> RegisterAsync(IdentityUser newUser,string password,MedicalRep rep);
        Task<RegisterServiceResult> LoginAsync(string email, string password);
        Task<RegisterServiceResult> RefreshTokenAsync(string token, string refreshToken);


    }

   
}