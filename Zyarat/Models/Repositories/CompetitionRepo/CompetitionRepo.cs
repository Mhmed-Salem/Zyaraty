using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.EntityFrameworkCore;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories;
using Zyarat.Models.Services.CompetitionService;

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
            var x = await Context.Competitions.FirstOrDefaultAsync(competition => competition.Type == type
                                                                           && competition.DateTime.Equals(
                                                                               new DateTime(year, month, day)));
            return x;
        }

        public async Task<Competition> GetNextCompetition(bool type)
        {
            return await Context.Competitions
                .Where(competition => competition.Type == type)
                .OrderByDescending(competition => competition.DateTime)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CompetitionWinner>> GetWinnersInCompetition(int competitionId)
        {
            return await Context.Winners
                .Include(winner => winner.MedicalRep)
                .ThenInclude(rep => rep.IdentityUser)
                .Where(winner => winner.CompetitionId==competitionId)
                .Select(winner => new CompetitionWinner
                {
                    Id = winner.MedicalRepId,
                    Rank = winner.Rank,
                    Phone = winner.MedicalRep.IdentityUser.PhoneNumber,
                    FName = winner.MedicalRep.FName,
                    LName = winner.MedicalRep.LName,
                    ImageUrl =winner.MedicalRep.ProfileUrl,
                    UserName = winner.MedicalRep.IdentityUser.UserName
                })
                .OrderBy(admin => admin.Rank)
                .ToListAsync();
        }
        
        public Competition ModifyCompetition(Competition newCompetition)
        {
             Context.Competitions.Update(newCompetition);
             return newCompetition;
        }

        public async Task<Competition> GetLastCompetition(bool type)
        {
            return await Context.Competitions.OrderByDescending(competition => competition.DateTime)
                .FirstOrDefaultAsync(competition => competition.Type==type);
        }

        public async Task<IEnumerable<Competitor>> GetCurrentResult(DateTime from,int minUniqueUsers,int minUniqueVisits)
        {
            var x = await Context.CurrentWinners.FromSqlInterpolated(
                    $"EXEC GetCompetitors @from={from}, @minUniqueUsers={minUniqueUsers},@minUniqueVisits={minUniqueVisits} ")
                .ToListAsync();
            return x;
        }

        public IEnumerable<Winner>GetFinalResult(int cId)
        {
            return Context.Winners.Include(c => c.Competition)
                .Include(winner => winner.MedicalRep)
                .Where(winner => winner.CompetitionId==cId);
        }

        public IEnumerable<CompetitionHacker> GetHackers(bool type, int top)
        {
            var data = Context.Winners
                .Include(winner => winner.Competition)
                .Include(winner => winner.MedicalRep)
                .ThenInclude(rep => rep.IdentityUser)
                .Where(winner => winner.Competition.Type == type && winner.Rank == 1)
                .Take(top)
                .Select(winner => new CompetitionHacker
                {
                    CompetitionId = winner.Competition.Id,
                    CompetitionType = winner.Competition.Type?CompetitionType.Monthly.ToString():CompetitionType.Daily.ToString(),
                    DateTime = winner.Competition.DateTime,
                    Hacker = new CompetitionWinner
                    {
                        Id = winner.MedicalRepId,
                        Phone = winner.MedicalRep.IdentityUser.PhoneNumber,
                        Rank = winner.Rank,
                        FName = winner.MedicalRep.FName,
                        LName = winner.MedicalRep.LName,
                        UserName = winner.MedicalRep.IdentityUser.UserName,
                        ImageUrl = winner.MedicalRep.ProfileUrl
                    }
                });
            return data;
        }
    }
}