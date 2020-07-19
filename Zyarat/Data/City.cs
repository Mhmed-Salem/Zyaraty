using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class City
    {
        public City()
        {
            Doctors = new List<Doctor>();
            MedicalReps = new List<MedicalRep>();
        }
        public int Id { get; set; }
        public int GovernmentId { get; set; }
        public string CityName { get; set; }
        public Government Government { set; get; }
        public List<Doctor> Doctors { set; get; }
        public List<MedicalRep> MedicalReps { set; get; }

    }
}
