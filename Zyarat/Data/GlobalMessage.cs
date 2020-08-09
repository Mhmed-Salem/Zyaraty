using System;
using System.Collections.Generic;

namespace Zyarat.Data
{
    public class GlobalMessage
    {
  
        public int Id { set; get; }
        public DateTime DateTime { set; get; }
        public int MessageContentId { set; get; }
        public MessageContent MessageContent { set; get; }
        
    }
}