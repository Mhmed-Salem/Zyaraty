using System;

namespace Zyarat.Models.DTO
{
    public class CompetitionHacker
    {
        public int CompetitionId { set; get; }
        public string CompetitionType { set; get; }
        public DateTime DateTime { set; get; }
        public CompetitionWinner Hacker { set; get; }
    }
}