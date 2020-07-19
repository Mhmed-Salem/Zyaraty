namespace Zyarat.Models.RequestResponseInteracting
{
    public class Response<T>
    {
        public string Error { set; get; }
        public T Source { set; get; }
        public bool Success => Error == null;

        public Response(string error)
        {
            Error = error;
        }

        public Response(T source, string error=null)
        {
            Source = source;
            Error = error;
        }
        
        
    }
}