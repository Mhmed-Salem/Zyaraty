using System.ComponentModel.DataAnnotations;

namespace Zyarat.Models.DTO
{
    public class AddEvaluationDto
    {
        [Required]
        public int  VisitId{ set; get; }
        [Required]
        public int EvaluatorId { set; get; }
        [Required]
        public bool Type { set; get; }
    }
}