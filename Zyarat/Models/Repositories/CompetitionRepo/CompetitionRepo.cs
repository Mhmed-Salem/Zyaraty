using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Repositories;

namespace Zyarat.Models.Repositories.CompetitionRepo
{
    public class CompetitionRepo:ContextRepo,ICompetitionRepo
    {
        public CompetitionRepo(ApplicationContext context) : base(context)
        {
        }

        public async Task AddCompetition(Competition competition)
        {
            await Context.Competitions.AddAsync(competition);
        }

        public async Task<Competition> GetCompetition(bool type, int year, int month, int day)
        {
            return await Context.Competitions.Where(competition =>
                    competition.Type && competition.DateTime >= new DateTime(year, month, day))
                .FirstOrDefaultAsync();
        }

     

        public Competition ModifyCompetition(Competition newCompetition)
        {
             Context.Competitions.Update(newCompetition);
             return newCompetition;
        }

        public async Task<Competition> GetLastCompetition(bool type)
        {
            return await Context.Competitions.OrderByDescending(competition => competition.DateTime).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Competitor>> GetCurrentResult(DateTime from,int minUniqueUsers,int minUniqueVisits)
        {
            return await Context.CurrentWinners.FromSqlInterpolated(
                    $"EXEC GetCompetitors @from={from}, @minUniqueUsers={minUniqueUsers},@minUniqueVisits={minUniqueVisits} ")
                .ToListAsync();
        }

        public IEnumerable<Winner>GetFinalResult(int cId)
        {
            return Context.Winners.Include(c => c.Competition)
                .Where(winner => winner.CompetitionId==cId);
        }

      
    }
}