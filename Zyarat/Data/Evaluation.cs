using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class Evaluation
    {
        public int Id { set; get; }
        public int VisitId { set; get; }
        public Visit Visit { set; get; }
        public int EvaluaterId { set; get; }//Medical rep who made the evaluation.
        public MedicalRep Evaluater { set; get; }//ref to Medical rep who made the evaluation object.
        public DateTime DateTime { set; get; }//the date and time the medical rep had made the evaluation at.
        /// <summary>
        /// the type of evaluation .it can be like (1) or Unlike (0)
        /// </summary>
        public bool Type { set; get; }
    }
}
