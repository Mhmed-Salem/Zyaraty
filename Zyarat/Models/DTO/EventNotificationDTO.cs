﻿using System;

namespace Zyarat.Models.DTO
{
    public class EventNotificationDto
    {
        public int Id { set; get; }
        public int MedicalRepId { set; get; }//the user who received Notification
        public DateTime DateTime { set; get; }
        public int DataId { set; get; }//the id of the entity 
        public int NotificationTypeId { set; get; }
        public string Message { set; get; }
        public bool Read { set; get; }
    }
}