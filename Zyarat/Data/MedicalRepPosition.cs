using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class MedicalRepPosition
    {
        public MedicalRepPosition() {
            MedicalReps = new List<MedicalRep>();
        }

        public int Id { set; get; }
        public string Title { set; get; }
        public List<MedicalRep> MedicalReps { set; get; }
       
    }
}
