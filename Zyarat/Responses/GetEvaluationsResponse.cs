namespace Zyarat.Responses
{
    public class GetEvaluationsResponse
    {
        public int  VisitId{ set; get; }
        public int EvaluatorId { set; get; }
        public bool Type { set; get; }
        public string Name { set; get; }
    }
}