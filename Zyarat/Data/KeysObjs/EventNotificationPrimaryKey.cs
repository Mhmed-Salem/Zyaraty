namespace Zyarat.Data.KeysObjs
{
    public class EventNotificationPrimaryKey
    {
        public EventNotificationPrimaryKey(int dataId, int typeId)
        {
            DataId = dataId;
            TypeId = typeId;
        }

        public int DataId { set; get; }
        public int TypeId { set; get; }
        public override int GetHashCode()             
        {  
            return DataId; 
        }
        public override bool Equals(object obj) 
        { 
            return Equals(obj as EventNotificationPrimaryKey); 
        }

        public bool Equals(EventNotificationPrimaryKey obj)
        { 
            return obj != null && obj.DataId == this.DataId; 
        }
    }
}