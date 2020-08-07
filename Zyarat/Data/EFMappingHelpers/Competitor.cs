using System;

namespace Zyarat.Data.EFMappingHelpers
{
    public class Competitor
    {
        public int Id { set; get; }
        public Int64 Ranking { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string Gov { set; get; }
        public string CityName { set; get; }
        public int UniqueVisits { set; get; }
        public int UniqueEvaluators { set; get; }
    }
}