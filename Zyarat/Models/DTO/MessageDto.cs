using System;
using System.Collections.Generic;
using Zyarat.Data.EFMappingHelpers;

namespace Zyarat.Models.DTO
{
    public class MessageDto
    {
        public MessageDto()
        {
            Receivers=new List<int>();
        }
        public int Id { set; get; }
        public string Content { set; get; }
        public DateTime DateTime { set; get; }
        public bool Read { set; get; }
        public string Type { set; get; }
        public List<int> Receivers { set; get; }
    }
}