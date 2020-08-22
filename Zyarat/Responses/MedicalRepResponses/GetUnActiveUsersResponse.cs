using System;

namespace Zyarat.Responses.MedicalRepResponses
{
    public class GetUnActiveUsersResponse
    {
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string ProfileUrl { set; get; }
        public string WorkedOnCompany { set; get; }
        public int CityId { set; get; }
        public int VisitsCount { set; get; }
        public int LikeCount { set; get;}
        public int DisLikeCount { set; get; }
        public int UniqueUsers { set; get; }
        public bool Active { set; get; }
    }
}