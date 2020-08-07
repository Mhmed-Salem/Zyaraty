namespace Zyarat.Data
{
    public class GlobalNotificationReading
    {
        public int Id { set; get; }
        public MedicalRep MedicalRep { set; get; }
        public int MedicalRepId { set; get; }
        public GlobalMessage GlobalMessage { set; get; }
        public int GlobalMessageId { set; get; }
    }
}