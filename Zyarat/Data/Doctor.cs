using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class Doctor
    {
        public Doctor()
        {
            Visits = new List<Visit>();
        }
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public int CityId { set; get; }
        public City City { set; get; }
        public int MedicalSpecializedId { set; get; }
        public MedicalSpecialized MedicalSpecialized { set; get; }
        public int AdderMedicalRepId { set; get; }
        public MedicalRep AdderMedicalRep { set; get; }
        public List<Visit> Visits { set; get; }

    }
}
