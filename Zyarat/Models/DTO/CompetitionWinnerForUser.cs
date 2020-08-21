using System;

namespace Zyarat.Models.DTO
{
    public class CompetitionWinnerForUser
    {
        public int CompetitionId { set; get; }
        public string CompetitionType { set; get; }
        public DateTime DateTime { set; get; }
        public WinnerForUser Hacker { set; get; }
    }
}