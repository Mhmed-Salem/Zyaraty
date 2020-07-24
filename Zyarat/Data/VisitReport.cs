namespace Zyarat.Data
{
    public class VisitReport
    {
        public int Id { set; get; }
        public int VisitId { set; get; }
        
        public Visit Visit { set; get; }
        public int ReporterId { set; get; }
        public MedicalRep Reporter { set; get; }
    }
}