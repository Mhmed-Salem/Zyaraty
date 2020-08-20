using System;
using System.Collections.Generic;

namespace Zyarat.Data
{
    public class GlobalMessage
    {
      public GlobalMessage()
        {
            Readings=new List<GlobalMessageReading>();
        }
        public int Id { set; get; }
        public DateTime DateTime { set; get; }
        public int MessageContentId { set; get; }
        public MessageContent MessageContent { set; get; }
        public List<GlobalMessageReading> Readings { set; get; }
        
    }
}