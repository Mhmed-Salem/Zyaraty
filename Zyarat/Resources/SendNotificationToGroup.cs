using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zyarat.Resources
{
    public class SendNotificationToGroup
    {
        [MaxLength(100),MinLength(5)]
        public string Content { set; get; }
        public List<int>Receivers { set; get; }
    }
}