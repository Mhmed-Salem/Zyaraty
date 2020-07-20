using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class Visit
    {
        public Visit()
        {
            Evaluation = new List<Evaluation>();
        }
        public int Id { set; get; }
        public MedicalRep MedicalRep { set; get; }
        public int MedicalRepId { set; get; }
        public int DoctorId { set; get; }
        public Doctor Doctor { set; get; }
        public string Content { set; get; }
        public DateTime DateTime { set; get; }
        public bool Type { set; get; }
        public List<Evaluation> Evaluation { set; get; }
        public bool Active { set; get; }
        
    }
}
