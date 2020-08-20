using System;
using System.ComponentModel.DataAnnotations;

namespace Zyarat.Data
{
    public class GlobalMessageReading
    {
        public int ReaderId { set; get; }
        public MedicalRep Reader { set; get; }
        public GlobalMessage GlobalMessage { set; get; }
        public int GlobalMessageId { set; get; }
    }
}