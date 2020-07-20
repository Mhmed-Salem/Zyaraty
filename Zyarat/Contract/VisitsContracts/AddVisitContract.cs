namespace Zyarat.Contract.VisitsContracts
{
    public class AddVisitContract
    {
        public string Content { set; get; }
        public int MedicalRepId { set; get; }
        public int DoctorId { set; get; }
        public bool Typ { set; get; }
    }
}