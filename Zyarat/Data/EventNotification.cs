using System;

namespace Zyarat.Data
{
    public class EventNotification
    {
        public int Id { set; get; }
        public int MedicalRepId { set; get; }//the user who received Notification
        public MedicalRep MedicalRep { set; get; } 
        public DateTime DateTime { set; get; }
        /**
         *EX) if the Notification type is evaluation then the DataId will be the
         * id of the evaluation in thee database 
         */
        public int DataId { set; get; }//the id of the entity 
        public NotificationType NotificationType { set; get; }
        public int NotificationTypeId { set; get; }
        public string Message { set; get; }
        public bool Read { set; get; }
    }
}