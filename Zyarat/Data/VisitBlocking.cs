using System;

namespace Zyarat.Data
{
    public class VisitBlocking
    {
        public int Id { set; get; }
        public DateTime BlockFrom { set; get; }
        public int MedicalRepId { set; get; }//blocked
        public MedicalRep MedicalRep { set; get; }
    }
}