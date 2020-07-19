using System.Collections.Generic;

namespace Zyarat.Resources
{
    public class RegisterServiceResult
    {
        public string Token { set; get; }
        public string RefreshToken { set; get; }
        public List<string> Errors { set; get; } = new List<string>();

        public bool Success => Errors.Count == 0;

        public RegisterServiceResult(string error)
        {
            Errors.Add(error);
        }

        public RegisterServiceResult()
        {

        }
    }
}