using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class MedicalSpecialized
    {
        public int Id { set; get; }
        public string Type { set; get; }
        public List<Doctor> Doctors { set; get; }
    }
}
