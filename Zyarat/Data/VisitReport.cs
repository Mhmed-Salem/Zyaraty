namespace Zyarat.Data
{
    public class VisitReport
    {
        public int Id { set; get; }
        public int VisitId { set; get; }
        public int MedicalRepId { set; get; }
        public MedicalRep MedicalRep { set; get; }
    }
}