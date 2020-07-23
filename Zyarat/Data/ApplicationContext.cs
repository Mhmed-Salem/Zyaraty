using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zyarat.Data
{
    public class ApplicationContext:IdentityDbContext
    {
        public ApplicationContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<City>Cities { set; get; }
        public DbSet<Doctor> Doctors { set; get; }
        public DbSet<Evaluation> Evaluations { set; get; }
        public DbSet<Government> Governments { set; get; }
        public DbSet<MedicalRep> MedicalReps { set; get; }
        public DbSet<MedicalRepPosition> MedicalRepPositions { set; get; }
        public DbSet<MedicalSpecialized> MedicalSpecializeds { set; get; }
        public DbSet<Visit> Visits { set; get; }
        public DbSet<RefreshingToken> RefreshingTokens { set; get; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //make the default value for the dateTime of adding the Evaluation equal the time it inserted in 
           builder.Entity<Evaluation>().Property(evaluation => evaluation.DateTime).HasDefaultValueSql("getdate()");



        }

    }
}
