using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Zyarat.Data;
using Zyarat.Models.Repositories.MedicalRepRepo;
using Zyarat.Options;
using Zyarat.Resources;

namespace Zyarat.Models.Services.IdentityServices
{
    public class IdentityService:IIdentityUser
    {
        
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor  _accessor;
        private readonly IMedicalRepRepo _usMedicalRepRepo;
        
        public IdentityService(JwtSettings jwtSettings, ApplicationContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor accessor, IMedicalRepRepo usMedicalRepRepo)
        {
            _jwtSettings = jwtSettings;
            _context = context;
            _userManager = userManager;
            _accessor = accessor;
            _usMedicalRepRepo = usMedicalRepRepo;
        }

        private async  Task<string> GenerateToken(IdentityUser user,MedicalRep rep)
        {
            var claims=new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", rep.Id.ToString()),
                new Claim("IsActive",rep.Active.ToString())
            };
            await AddRolesToPrincipalClaims(user, claims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var jwt = new JwtSecurityToken(
                null,
                null,
                 claims: claims,
                DateTime.UtcNow,
                expires:  DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(key: key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        
        

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        private ClaimsPrincipal GetClaimPrincipalFromExpiredTokens(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken==null||!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }


        private async Task AddRefreshTokenAsync(RefreshingToken refreshingToken)
        {
            await _context.RefreshingTokens.AddAsync(refreshingToken);
            await _context.SaveChangesAsync();
        }

        private async Task UpdateRefreshToken(string oldRefreshToken,string newRefreshToken)
        {
            var row =await  _context.RefreshingTokens
                .SingleOrDefaultAsync(token => token.RefreshToken == oldRefreshToken);
            row.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
        }

        public async Task<RegisterServiceResult> RegisterAsync(IdentityUser newUser, string password,MedicalRep rep)
        {
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user!=null)
            {
                return new RegisterServiceResult("the user is already existed !");
            }

            var state=await _userManager.CreateAsync(newUser, password);
            if (!state.Succeeded)
            {
                return new RegisterServiceResult
                {
                    Errors = state.Errors.Select(error => error.Description).ToList()
                };
            }

            var refreshToken = GenerateRefreshToken();
            await AddRefreshTokenAsync(new RefreshingToken
            {
                IP =_accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                RefreshToken = refreshToken,
                UserId =newUser.Id,
            });
           
            return new RegisterServiceResult
            {
                Token = await GenerateToken( newUser,rep),
                RefreshToken = refreshToken
            };
        }

        private async Task AddRolesToPrincipalClaims(IdentityUser user,List<Claim> claims)
        {
           claims.AddRange(from s in await _userManager.GetRolesAsync(user) select new Claim(ClaimTypes.Role, s));
           //add claims 
           claims.AddRange(from userClaim in await _userManager.GetClaimsAsync(user) select new Claim(userClaim.Type,userClaim.Value) );
        }
        

        public async Task<RegisterServiceResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user==null) return new RegisterServiceResult("UserName is Invalid.");
            var relatedData = await _usMedicalRepRepo.GetUserByIdentityIdAsync(user.Id);
            if (!await _userManager.CheckPasswordAsync(user,password))
            {
                return new RegisterServiceResult("Password is not Valid.");
            }

            var newRefreshToken = GenerateRefreshToken();
            
            await AddRefreshTokenAsync(new RefreshingToken
            {
                IP =_accessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                RefreshToken = newRefreshToken,
                UserId =user.Id,
            });
            return new RegisterServiceResult{
                RefreshToken = newRefreshToken, 
                Token = await GenerateToken(user,relatedData)
            };
        }

        public async Task<RegisterServiceResult> RefreshTokenAsync(string token,string refreshToken)
        {
            var claimsPrincipal = GetClaimPrincipalFromExpiredTokens(token);
            var newRefreshToken = GenerateRefreshToken();
            if (await _context.RefreshingTokens.AsNoTracking().FirstOrDefaultAsync(refreshingToken => refreshingToken.RefreshToken==refreshToken)==null)
            {
                return new RegisterServiceResult("Invalid Refresh Token");
            }
            var relatedData = await _usMedicalRepRepo.GetUserByIdentityIdAsync(claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type=="id")?.Value);

            await UpdateRefreshToken(refreshToken,newRefreshToken);
            return new RegisterServiceResult
            {
                Token = await GenerateToken(await _userManager.FindByEmailAsync(
                    claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value),relatedData),
                RefreshToken = refreshToken
            };
        }
    }
}