using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Repositories.CompetitionRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.CompetitionService
{
    public interface ICompetitionService
    {
        Task<Response<Competition>> AddNextCompetition(Competition competition);
        Task<Response<Competition>> ModifyNextCompetition(int id,Competition competition);
        Task<Response<IEnumerable<Competitor>>> GetCurrentResult(CompetitionType type,int userId);
        Task<Response<IEnumerable<Winner>>> GetFinalResult(CompetitionType type, int year, int month, int day);
    }
}