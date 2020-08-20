using System;

namespace Zyarat.Data
{
    public class Message
    {
        public int Id { set; get; }
        public DateTime DateTime { set; get; }
        public MedicalRep Receiver { set; get; }
        public int ReceiverId { set; get; }
        public MessageContent Content { set; get; }
        public int ContentId { set; get; }
        public bool Read { set; get; }
    }
}