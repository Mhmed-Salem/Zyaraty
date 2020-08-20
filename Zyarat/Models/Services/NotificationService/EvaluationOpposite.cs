using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using Zyarat.Data;

namespace Zyarat.Models.Services.NotificationService
{
    public class EvaluationOpposite:INotificationOpposite
    {
        private readonly EventNotification _notification;
       
        

        public EvaluationOpposite(EventNotification notification)
        {
            _notification = notification;
        }

        public void Opposite()
        {
            var builder=new StringBuilder();
            var m = _notification.Message.ToLower();
            var likeFound = false;
            var dislikeFound = false;
            for (var i = 0; i < m.Length; i++)
            {
               
                    if (!likeFound&&m[i] == 'd' && m[i + 1] == 'i' && m[i + 2] == 's') 
                    {
                        i += 2;
                        dislikeFound = true; 
                    }
                    else if (!dislikeFound&&m[i] == 'l' && m[i + 1] == 'i' && m[i + 2] == 'k' && m[i + 3] == 'e') 
                    { 
                        builder.Append("dislike");
                        i += 3;
                        likeFound = true;
                    } 
                    else builder.Append(m[i]);
                
            }

            _notification.Message = builder.ToString();
        }
    }
}