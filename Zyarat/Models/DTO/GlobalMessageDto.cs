using System;
using System.Collections.Generic;

namespace Zyarat.Models.DTO
{
    public class GlobalMessageDto
    {
       
        public int Id { set; get; }
        public string Content { set; get; }
        public DateTime DateTime { set; get; }
        public bool Read { set; get; }
        public string Type { set; get; }
    }
}