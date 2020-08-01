using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;

namespace Zyarat.Models.Repositories.CompetitionRepo
{
    public interface ICompetitionRepo
    {
         Task AddCompetition(Competition competition);
         Task<Competition> GetCompetition(bool type, int year, int month, int day);
         Competition ModifyCompetition(Competition newCompetition);
         Task<Competition> GetLastCompetition(bool type);
         Task<IEnumerable<Competitor>> GetCurrentResult(DateTime from, int minUniqueUsers, int minUniqueVisits);
         IEnumerable<Winner> GetFinalResult(int cId);
    }
}