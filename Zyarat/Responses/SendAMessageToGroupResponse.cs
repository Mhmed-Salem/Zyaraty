using System.Collections.Generic;

namespace Zyarat.Responses
{
    public class SendAMessageToGroupResponse
    {
        public int ContentId { set; get; }
        public string Content { set; get; }
        public List<int> Receivers { set; get; }
    }
}