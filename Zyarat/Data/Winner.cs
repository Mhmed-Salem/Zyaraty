namespace Zyarat.Data
{
    public class  Winner
    { 
        public int Id { set; get; }
        public int CompetitionId { set; get; }
        public Competition Competition { set; get; }
        public int Rank { set; get; }
        public int MedicalRepId { set; get; }
        public MedicalRep MedicalRep { set; get; }
    }
}