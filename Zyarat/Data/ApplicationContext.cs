using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Zyarat.Data.EFMappingHelpers;

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
        public DbSet<Competition> Competitions { set; get; }
        public DbSet<Winner> Winners { set; get; }
        public DbSet<Competitor> CurrentWinners { set; get; }
        
        public DbSet<Notification> Notifications { set; get; }
        public DbSet<NotificationType> NotificationTypes { set; get; }
        public DbSet<MessageContent> MessageContents { set; get; }
        public DbSet<GlobalMessage> GlobalMessages { set; get; }
        public DbSet<GlobalNotificationReading> GlobalNotificationReadings { set; get; }








        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /**
             * All the foreign key relationships un database is No action.
             * except for the Evaluation_Visit_FK as it's a Cascade, and VisitReports
             */
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            builder.Entity<Visit>().HasMany(v => v.Evaluation)
                .WithOne(evaluation => evaluation.Visit)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Visit>().HasMany(v => v.VisitReports)
                .WithOne(report => report.Visit)
                .OnDelete(DeleteBehavior.Cascade);
            
            //make the default value for the dateTime of adding the Evaluation equal the time it inserted in 
           builder.Entity<Evaluation>().Property(evaluation => evaluation.DateTime).HasDefaultValueSql("getdate()");

           builder.Entity<MedicalRep>().Property(rep => rep.Active).HasDefaultValue(true);
           builder.Entity<MedicalRep>().Property(rep => rep.PermanentDeleted).HasDefaultValue(false);

           builder.Entity<Competitor>().HasNoKey();
           
           /**Seed NotificationType data*/
           builder.Entity<NotificationType>().HasData(new NotificationType()
           {
               Id = 1,
               Type = "evaluation",
               Template = "{UserName} make a {like/dislike} to you comment in Dr/{doctorName}"
           });
           builder.Entity<NotificationType>().HasData(new NotificationType()
           {
               Id = 2,
               Type = "message",
               Template = "{message Content}"
           });
           builder.Entity<NotificationType>().HasData(new NotificationType()
           {
               Id = 3,
               Type = "globalMessage",
               Template = "{message Content}"
           });


        }

    }
}
