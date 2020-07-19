namespace Zyarat.Models.DTO
{
    public class EvaluationDto
    {
        public int Id { set; get; }
        public bool Type { set; get; }
        public MedicalRepDto Evaluater { set; get; }//ref to Medical rep who made the evaluation object.
        
    }
}