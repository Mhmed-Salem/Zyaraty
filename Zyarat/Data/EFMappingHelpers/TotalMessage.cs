using System;
using Zyarat.Models.Factories;

namespace Zyarat.Data.EFMappingHelpers
{
    public class TotalMessage
    {
        public int Id { set; get; }
        public string Content { set; get; }
        public NotificationTypesEnum TypesEnum { set; get; }
        public DateTime DateTime { set; get; }
    }
}