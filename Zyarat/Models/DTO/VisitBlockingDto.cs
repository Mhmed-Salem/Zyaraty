using System;

namespace Zyarat.Models.DTO
{
    public class VisitBlockingDto
    {
        public int Id { set; get; }
        public DateTime BlockFrom { set; get; }
        public DateTime BlockTo { set; get; }
    }
}