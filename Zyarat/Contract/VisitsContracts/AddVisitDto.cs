namespace Zyarat.Contract.VisitsContracts
{
    public class AddVisitDto
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public int MedicalRepId { set; get; }
        public int DoctorId { set; get; }
        public bool Typ { set; get; }
    }
}