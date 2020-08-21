using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zyarat.Data;
using Zyarat.Data.EFMappingHelpers;
using Zyarat.Models.DTO;
using Zyarat.Models.Repositories.CompetitionRepo;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services.CompetitionService
{
    public class CompetitionService:ICompetitionService
    {
        private readonly ICompetitionRepo _repo;
        private readonly IUnitWork _unitWork;
        private const int ReturnedRowNumber = 20;
        private const int HourOffset = 2;
        private readonly TimeOffSetHandler _offSetHandler;

        public CompetitionService(ICompetitionRepo repo, IUnitWork unitWork)
        {
            _repo = repo;
            _unitWork = unitWork;
            _offSetHandler=new TimeOffSetHandler(HourOffset);
        }

        public async Task<Response<Competition>> AddNextCompetition(Competition competition)
        {
            try
            {
                var last =await _repo.GetLastCompetition(competition.Type);
                var now = _offSetHandler.GetDate();
                var d1=new DateTime(now.Year,now.Month,now.Day).AddDays(1);//for daily comparison
                var d2=new DateTime(now.Year,now.Month,1).AddDays(1);//for monthly comparision
                if (last!=null&&(!competition.Type && d1 <= last.DateTime.Date
                                  || competition.Type && d2 <= last.DateTime.Date))
                {
                    return new Response<Competition>("the Competition already exists ! ");
                }

                competition.DateTime = competition.Type ? d2 : d1;
                await _repo.AddCompetition(competition);
                await _unitWork.CommitAsync();
                return new Response<Competition>(competition);
            }
            catch (Exception e)
            {
                return new Response<Competition>($"Error :{e.Message}");
            }
        }

        public async Task<Response<Competition>> AddNextCompetition_Test(Competition competition, DateTime addDateTime)
        {
            try
            {
                var last =await _repo.GetLastCompetition(competition.Type);
                var now = _offSetHandler.GetDate(addDateTime);
                var d1=new DateTime(now.Year,now.Month,now.Day).AddDays(1);//for daily comparison
                var d2=new DateTime(now.Year,now.Month,1).AddDays(1);//for monthly comparision
                if (last!=null&&(!competition.Type && d1 <= last.DateTime.Date
                                 || competition.Type && d2 <= last.DateTime.Date))
                {
                    return new Response<Competition>("the Competition already exists ! ");
                }
                competition.DateTime = competition.Type ? d2 : d1;
                await _repo.AddCompetition(competition);
                await _unitWork.CommitAsync();
                return new Response<Competition>(competition);
            }
            catch (Exception e)
            {
                return new Response<Competition>($"Error :{e.Message}");
            }
        }

        public async Task<Response<Competition>> GetNextCompetition(CompetitionType type)
        {
            try
            {
                var now = _offSetHandler.GetDate();
                var d1 = new DateTime(now.Year, now.Month, now.Day).AddDays(1);
                var d2 = new DateTime(now.Year, now.Month, 1).AddDays(1);
                var d = CompetitionType.Monthly == type ? d2 : d1;
                var c = await _repo.GetCompetition(CompetitionType.Monthly == type, d.Year, d.Month, d.Day);
                return c==null ? new Response<Competition>("you have not added the next competition Yet") : new Response<Competition>(c);
            }
            catch (Exception e)
            {
              return new Response<Competition>($"Error:{e.Message}");
            }
        }

        public Response<IEnumerable<CompetitionHacker>> GetHackers(CompetitionType type,int top)
        {
            try
            {
                var hackers = _repo.GetHackers(type==CompetitionType.Monthly,top);
                return hackers == null
                    ? new Response<IEnumerable<CompetitionHacker>>("No Competition Yet!")
                    : new Response<IEnumerable<CompetitionHacker>>(hackers);

            }
            catch (Exception e)
            {
                return new Response<IEnumerable<CompetitionHacker>>($"Error :{e.Message}");
            }
        }


        public async  Task<Response<Competition>> ModifyNextCompetition(Competition competition)
        {
            try
            {
                var c =await _repo.GetLastCompetition(competition.Type);
                var now=_offSetHandler.GetDate();
                var d1=new DateTime(now.Year,now.Month,now.Day);//for daily comparison
                var d2=new DateTime(now.Year,now.Month,1);//for monthly comparision
                if (c==null || (competition.Type && !d2.AddMonths(1).Equals(c.DateTime)
                    ||!competition.Type && !d1.AddDays(1).Equals(c.DateTime)))
                {
                    return new Response<Competition>("the Competition does not exist ! ");
                }
                
                c.Roles = competition.Roles;
                c.MinUniqueUsers = competition.MinUniqueUsers;
                c.MinUniqueVisits = competition.MinUniqueVisits;
                
                var newCompetition=_repo.ModifyCompetition(c);

                await _unitWork.CommitAsync();
                return new Response<Competition>(newCompetition);

            }
            catch (Exception e)
            { 
                return new Response<Competition>($"Error :{e.Message}");
            }
        }

        public async  Task<Response<Competition>> ModifyNextCompetition_Test(Competition competition, DateTime modifyTime)
        {
            try
            {
                var c =await _repo.GetLastCompetition(competition.Type);
                var now=_offSetHandler.GetDate(modifyTime);
                var d1=new DateTime(now.Year,now.Month,now.Day);//for daily comparison
                var d2=new DateTime(now.Year,now.Month,1);//for monthly comparision
                if (c==null || (competition.Type && !d2.AddMonths(1).Equals(c.DateTime)
                                ||!competition.Type && !d1.AddDays(1).Equals(c.DateTime)))
                {
                    return new Response<Competition>("the Competition does not exist ! ");
                }
                
                c.Roles = competition.Roles;
                c.MinUniqueUsers = competition.MinUniqueUsers;
                c.MinUniqueVisits = competition.MinUniqueVisits;
                
                var newCompetition=_repo.ModifyCompetition(c);

                await _unitWork.CommitAsync();
                return new Response<Competition>(newCompetition);

            }
            catch (Exception e)
            { 
                return new Response<Competition>($"Error :{e.Message}");
            }        }

        public async  Task<Response<IEnumerable<Competitor>>> GetCurrentResult(CompetitionType type,int repId)
        {
            try
            {
                var last = await _repo.GetLastCompetition(type != CompetitionType.Daily);
                var compareDate = _offSetHandler.GetDate();
                if (last==null||!compareDate.Equals(last.DateTime))
                {
                    return new Response<IEnumerable<Competitor>>("No Competition!");
                }
                var allCompetitors = await _repo.GetCurrentResult(
                    new DateTime(last.DateTime.Year, last.DateTime.Month, last.DateTime.Day, HourOffset, 0, 0),
                    last.MinUniqueUsers,
                    last.MinUniqueVisits);
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

        public async Task<Response<IEnumerable<CompetitionWinner>>> GetFinalResult(CompetitionType type, int year, int month, int day)
        {
            try
            {
                var c = await _repo.GetCompetition(type == CompetitionType.Monthly, year, month, day);
                return c==null ? new Response<IEnumerable<CompetitionWinner>>("th competition Is not existed!") : 
                    new Response<IEnumerable<CompetitionWinner>>(await _repo.GetWinnersInCompetition(c.Id));
            }
            catch (Exception e)
            {
                return new Response<IEnumerable<CompetitionWinner>>($"Error :{e.Message}");
            }
        }
        
        
        
    }
}