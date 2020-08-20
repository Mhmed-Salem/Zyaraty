using System;
using System.Collections.Generic;

namespace Zyarat.Data
{
    public class Competition
    {
        public Competition ()
        {
            Winners=new List<Winner>();
        }
        public int Id { set; get; }
        public DateTime DateTime { set; get; }
        public string Roles { set; get; }
        /// <summary>
        /// 1 for monthly competition
        /// 0 for daily Competition
        /// </summary>
        public bool Type { set; get; }
        public int MinUniqueUsers { set; get; }
        public int MinUniqueVisits { set; get; }
        public List<Winner> Winners { set; get; }
    }
}