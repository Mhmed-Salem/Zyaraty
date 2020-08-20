namespace Zyarat.Models.Services.NotificationService
{
    public  class MessageKey
    {
        public int Type { set; get; }
        public int Id { set; get; }
        
        public override int GetHashCode()             
        {  
            return Id; 
        }
        public override bool Equals(object obj) 
        { 
            return Equals(obj as MessageKey); 
        }

        public bool Equals(MessageKey obj)
        {
            return obj != null && obj.Id == this.Id;
            
        }
    }
}