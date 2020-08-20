using System.Collections.Generic;

namespace Zyarat.Responses
{
    public class SendAMessageToGroupResponse
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public List<int> Receivers { set; get; }
    }
}