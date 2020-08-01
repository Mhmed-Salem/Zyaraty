using System.Collections.Generic;

namespace Zyarat.Models.DTO
{
    public class GovernmentDto
    {
        public GovernmentDto()
        {
            Cities=new List<CityDto>();
        }
        public int Id { set; get; }
        public string Gov { set; get; }
        public List<CityDto> Cities { set; get; }
    }
}