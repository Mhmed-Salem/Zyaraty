using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class Government
    {
        public Government()
        {
            Cities = new List<City>();
        }
        public int Id { set; get; }
        public string Gov { set; get; }
        public List<City> Cities { set; get;}
    }
}
