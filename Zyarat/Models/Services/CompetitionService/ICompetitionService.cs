using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.CompetitionRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.CompetitionService
{
    public interface ICompetitionService
    {
        Task<Response<Competition>> AddNextCompetition(Competition competition);
        Task<Response<Competition>> AddNextCompetition_Test(Competition competition,DateTime addDateTime);
        Task<Response<Competition>> GetNextCompetition(CompetitionType type);
        Response<IEnumerable<CompetitionHacker>> GetHackers(CompetitionType type, int top);

        Task<Response<Competition>> ModifyNextCompetition(Competition competition);
        Task<Response<Competition>> ModifyNextCompetition_Test(Competition competition,DateTime modifyTime);

        Task<Response<IEnumerable<Competitor>>> GetCurrentResult(CompetitionType type,int userId);
        Task<Response<IEnumerable<CompetitionWinner>>> GetFinalResult(CompetitionType type, int year, int month, int day);
    }
}