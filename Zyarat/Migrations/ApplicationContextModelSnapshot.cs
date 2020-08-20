﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zyarat.Data;

namespace Zyarat.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Zyarat.Data.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GovernmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GovernmentId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Zyarat.Data.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MinUniqueUsers")
                        .HasColumnType("int");

                    b.Property<int>("MinUniqueVisits")
                        .HasColumnType("int");

                    b.Property<string>("Roles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Zyarat.Data.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdderMedicalRepId")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedicalSpecializedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdderMedicalRepId");

                    b.HasIndex("CityId");

                    b.HasIndex("MedicalSpecializedId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Zyarat.Data.EFMappingHelpers.Competitor", b =>
                {
                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gov")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Ranking")
                        .HasColumnType("bigint");

                    b.Property<int>("UniqueEvaluators")
                        .HasColumnType("int");

                    b.Property<int>("UniqueVisits")
                        .HasColumnType("int");

                    b.ToTable("CurrentWinners");
                });

            modelBuilder.Entity("Zyarat.Data.EFMappingHelpers.CountMessages", b =>
                {
                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.ToTable("CountMessages");
                });

            modelBuilder.Entity("Zyarat.Data.Evaluation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("EvaluatorId")
                        .HasColumnType("int");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.Property<int>("VisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EvaluatorId");

                    b.HasIndex("VisitId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("Zyarat.Data.EventNotification", b =>
                {
                    b.Property<int>("DataId")
                        .HasColumnType("int");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicalRepId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotificationTypeType")
                        .HasColumnType("int");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.HasKey("DataId", "NotificationTypeId");

                    b.HasIndex("NotificationTypeType");

                    b.HasIndex("MedicalRepId", "DateTime")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("EventNotifications");
                });

            modelBuilder.Entity("Zyarat.Data.GlobalMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MessageContentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateTime")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("MessageContentId");

                    b.ToTable("GlobalMessages");
                });

            modelBuilder.Entity("Zyarat.Data.GlobalMessageReading", b =>
                {
                    b.Property<int>("ReaderId")
                        .HasColumnType("int");

                    b.Property<int>("GlobalMessageId")
                        .HasColumnType("int");

                    b.HasKey("ReaderId", "GlobalMessageId");

                    b.HasIndex("GlobalMessageId");

                    b.ToTable("GlobalMessageReading");
                });

            modelBuilder.Entity("Zyarat.Data.Government", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Gov")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Governments");
                });

            modelBuilder.Entity("Zyarat.Data.MedicalRep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeActiveDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisLikeCount")
                        .HasColumnType("int");

                    b.Property<string>("FName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LikeCount")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRepPositionId")
                        .HasColumnType("int");

                    b.Property<bool>("PermanentDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("ProfileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniqueUsers")
                        .HasColumnType("int");

                    b.Property<int>("VisitsCount")
                        .HasColumnType("int");

                    b.Property<string>("WorkedOnCompany")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("IdentityUserId");

                    b.HasIndex("MedicalRepPositionId");

                    b.ToTable("MedicalReps");
                });

            modelBuilder.Entity("Zyarat.Data.MedicalRepPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalRepPositions");
                });

            modelBuilder.Entity("Zyarat.Data.MedicalSpecialized", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MedicalSpecializeds");
                });

            modelBuilder.Entity("Zyarat.Data.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Read")
                        .HasColumnType("bit");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("ReceiverId", "DateTime")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Zyarat.Data.MessageContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotificationTypeId")
                        .HasColumnType("int");

                    b.Property<int>("NotificationTypeType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeType");

                    b.ToTable("MessageContents");
                });

            modelBuilder.Entity("Zyarat.Data.NotificationType", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Template")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Type");

                    b.ToTable("NotificationTypes");

                    b.HasData(
                        new
                        {
                            Type = 0,
                            Template = "{UserName} had made a {like/dislike} to your comment in Dr/{doctorName} :  \"{visit}\" "
                        },
                        new
                        {
                            Type = 1,
                            Template = "{message Content}"
                        },
                        new
                        {
                            Type = 2,
                            Template = "{message Content}"
                        });
                });

            modelBuilder.Entity("Zyarat.Data.RefreshingToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshingTokens");
                });

            modelBuilder.Entity("Zyarat.Data.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRepId")
                        .HasColumnType("int");

                    b.Property<bool>("Type")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MedicalRepId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Zyarat.Data.VisitReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ReporterId")
                        .HasColumnType("int");

                    b.Property<int>("VisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReporterId");

                    b.HasIndex("VisitId");

                    b.ToTable("VisitReport");
                });

            modelBuilder.Entity("Zyarat.Data.Winner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionId")
                        .HasColumnType("int");

                    b.Property<int>("MedicalRepId")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("MedicalRepId");

                    b.ToTable("Winners");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.City", b =>
                {
                    b.HasOne("Zyarat.Data.Government", "Government")
                        .WithMany("Cities")
                        .HasForeignKey("GovernmentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.Doctor", b =>
                {
                    b.HasOne("Zyarat.Data.MedicalRep", "AdderMedicalRep")
                        .WithMany("AdderDoctor")
                        .HasForeignKey("AdderMedicalRepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.City", "City")
                        .WithMany("Doctors")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.MedicalSpecialized", "MedicalSpecialized")
                        .WithMany("Doctors")
                        .HasForeignKey("MedicalSpecializedId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.Evaluation", b =>
                {
                    b.HasOne("Zyarat.Data.MedicalRep", "Evaluator")
                        .WithMany()
                        .HasForeignKey("EvaluatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.Visit", "Visit")
                        .WithMany("Evaluation")
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.EventNotification", b =>
                {
                    b.HasOne("Zyarat.Data.MedicalRep", "MedicalRep")
                        .WithMany()
                        .HasForeignKey("MedicalRepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.NotificationType", "NotificationType")
                        .WithMany("Notifications")
                        .HasForeignKey("NotificationTypeType")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.GlobalMessage", b =>
                {
                    b.HasOne("Zyarat.Data.MessageContent", "MessageContent")
                        .WithMany()
                        .HasForeignKey("MessageContentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.GlobalMessageReading", b =>
                {
                    b.HasOne("Zyarat.Data.GlobalMessage", "GlobalMessage")
                        .WithMany("Readings")
                        .HasForeignKey("GlobalMessageId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.MedicalRep", "Reader")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.MedicalRep", b =>
                {
                    b.HasOne("Zyarat.Data.City", "City")
                        .WithMany("MedicalReps")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Zyarat.Data.MedicalRepPosition", "MedicalRepPosition")
                        .WithMany("MedicalReps")
                        .HasForeignKey("MedicalRepPositionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.Message", b =>
                {
                    b.HasOne("Zyarat.Data.MessageContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.MedicalRep", "Receiver")
                        .WithMany("Messages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.MessageContent", b =>
                {
                    b.HasOne("Zyarat.Data.NotificationType", "NotificationType")
                        .WithMany()
                        .HasForeignKey("NotificationTypeType")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.RefreshingToken", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "MyIdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Zyarat.Data.Visit", b =>
                {
                    b.HasOne("Zyarat.Data.Doctor", "Doctor")
                        .WithMany("Visits")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.MedicalRep", "MedicalRep")
                        .WithMany("Visits")
                        .HasForeignKey("MedicalRepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.VisitReport", b =>
                {
                    b.HasOne("Zyarat.Data.MedicalRep", "Reporter")
                        .WithMany("VisitReports")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.Visit", "Visit")
                        .WithMany("VisitReports")
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Zyarat.Data.Winner", b =>
                {
                    b.HasOne("Zyarat.Data.Competition", "Competition")
                        .WithMany("Winners")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Zyarat.Data.MedicalRep", "MedicalRep")
                        .WithMany()
                        .HasForeignKey("MedicalRepId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
