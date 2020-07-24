using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class MedicalRep
    {
        public MedicalRep()
        {
            Visits = new List<Visit>();
            AdderDoctor = new List<Doctor>();
            VisitReports=new List<VisitReport>();
        }
        public int Id { set; get; }
        public string FName { set; get; }
        public string LName { set; get; }
        public string ProfileUrl { set; get; }
        public IdentityUser IdentityUser { set; get; }
        public string IdentityUserId { set; get; }
        public string WorkedOnCompany { set; get; }
        public int CityId { set; get; }
        public City City { set; get; }
        public int VisitsCount { set; get; }
        public int LikeCount { set; get;}
        public int DisLikeCount { set; get; }
        public int UniqueUsers { set; get; }
        public bool Active { set; get; }
        public DateTime DeActiveDate { set; get; }
        public bool PermanentDeleted { set; get; }
        
        public int MedicalRepPositionId { set; get; }
        public MedicalRepPosition MedicalRepPosition { set; get; }
        public List<Visit> Visits { set; get; }
        public List<Doctor> AdderDoctor { set; get; }//Doctors added by this User
        public List<VisitReport> VisitReports { set; get; }
        

        public string GenerateUserName()
        {
            return $"{FName}_{LName}{new Random().Next(1, 10000).ToString()}";
        }
    }
}
