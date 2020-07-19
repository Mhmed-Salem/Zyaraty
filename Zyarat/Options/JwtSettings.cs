using System;

namespace Zyarat.Options
{
    public class JwtSettings
    {
        public string Secret { set; get; }
        public TimeSpan TokenLifeTime { set; get; }
    }
}