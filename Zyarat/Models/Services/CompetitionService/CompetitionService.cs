﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.Repositories.CompetitionRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.CompetitionService
{
    public class CompetitionService:ICompetitionService
    {
        private readonly ICompetitionRepo _repo;
        private readonly IUnitWork _unitWork;
        private const int ReturnedRowNumber = 20;

        public CompetitionService(ICompetitionRepo repo, IUnitWork unitWork)
        {
            _repo = repo;
            _unitWork = unitWork;
        }

        public async Task<Response<Competition>> AddNextCompetition(Competition competition)
        {
            try
            {
                var c =await _repo.GetLastCompetition(competition.Type);
                if (!competition.Type&&DateTime.Now.Date.AddDays(1)<=c.DateTime.Date
                    ||competition.Type&&DateTime.Now.Date.AddMonths(1)<=c.DateTime.Date)
                {
                    return new Response<Competition>("the Competition already exists ! ");
                }

                if (competition.Type)
                {
                    competition.DateTime=new DateTime(competition.DateTime.Year,competition.DateTime.Month,1);
                }
                await _repo.AddCompetition(competition);
                await _unitWork.CommitAsync();
                return new Response<Competition>(competition);
            }
            catch (Exception e)
            {
                return new Response<Competition>($"Error :{e.Message}");
            }
        }

     

        public async  Task<Response<Competition>> ModifyNextCompetition(int id,Competition competition)
        {
            try
            {
                var c =await _repo.GetLastCompetition(competition.Type);
                if (DateTime.Now.Date.AddDays(1)>c.DateTime.Date)
                {
                    return new Response<Competition>("the Competition does not exist ! ");
                }

                c.Roles = competition.Roles;
                c.MinUniqueUser = competition.MinUniqueUser;
                c.MinUniqueVisit = competition.MinUniqueVisit;
                
                var newCompetition=_repo.ModifyCompetition(c);

                await _unitWork.CommitAsync();
                return new Response<Competition>(newCompetition);

            }
            catch (Exception e)
            { 
                return new Response<Competition>($"Error :{e.Message}");
            }
        }

        public async  Task<Response<IEnumerable<Competitor>>> GetCurrentResult(CompetitionType type,int repId)
        {
            try
            {
                var last = await _repo.GetLastCompetition(type != CompetitionType.Daily);
                var allCompetitors = await _repo.GetCurrentResult(
                    from: new DateTime(last.DateTime.Year, last.DateTime.Month, last.DateTime.Day, 2, 0, 0),
                    last.MinUniqueUser,
                    last.MinUniqueVisit);
                /**
                 * take the top @ReturnedRowNumber of rows with the  rank of  a specific User
                 * in the competition .if the passed user is not in the competition ,it will
                 * only return the top @ReturnedRowNumber of rows.
                 **/
                var rt=new List<Competitor>();
                var found = false;
                foreach (var competitor in allCompetitors)
                {
                    if (found && competitor.Ranking > ReturnedRowNumber) break;
                    if (competitor.Ranking > ReturnedRowNumber && (found || competitor.Id != repId)) continue;
                    rt.Add(competitor);
                    if (!found&&competitor.Id==repId)
                    {
                        found = true;
                    }
                }
                //end of result filtering
                return new Response<IEnumerable<Competitor>>(rt);
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<Competitor>>($"Error :{e.Message}");
            }
        }

        public async  Task<Response<IEnumerable<Winner>>> GetFinalResult(CompetitionType type, int year, int month, int day)
        {
            try
            {
                var c = await _repo.GetCompetition(type == CompetitionType.Monthly, year, month, day);
                return c==null ? new Response<IEnumerable<Winner>>("th competition Is not existed!") : 
                    new Response<IEnumerable<Winner>>(_repo.GetFinalResult(c.Id));
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<Winner>>($"Error :{e.Message}");
            }
        }
    }
}