﻿namespace Zyarat.Models.DTO
{
    public class EvaluationDto
    {
        public int Id { set; get; }
        public bool Type { set; get; }
        public int  VisitId { set; get; }//ref to Medical rep who made the evaluation object.
        
    }
}