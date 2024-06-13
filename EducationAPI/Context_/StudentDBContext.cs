using System;
using System.Collections.Generic;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Context;

public partial class StudentDBContext : DbContext
{
    public StudentDBContext()
    {
    }

    public StudentDBContext(DbContextOptions<StudentDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditingSession> AuditingSessions { get; set; }

    public virtual DbSet<Auditor> Auditors { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<CollegeSection> CollegeSections { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    //public virtual DbSet<CoursePlan> CoursePlans { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<ProviderCenter> ProviderCenters { get; set; }

    //public virtual DbSet<ProviderStudyGroup> ProviderStudyGroups { get; set; }

    //public virtual DbSet<StudentTestContext> StudentTests { get; set; }

    public virtual DbSet<StudyGroup> StudyGroups { get; set; }

    public virtual DbSet<StudyGroupSession> StudyGroupSessions { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrainingCenter> TrainingCenters { get; set; }

    public virtual DbSet<TrainingProvider> TrainingProviders { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<AuditorRoundCodeAssignment> AuditorRoundCodeAssignments { get; set; }

    public static readonly Microsoft.Extensions.Logging.LoggerFactory _myLoggerFactory =
    new LoggerFactory(new[] {
        new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider()
    });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       optionsBuilder.UseSqlServer("Server=DESKTOP-GBPLSPS;Database=StudentDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;User ID=sherin;Password=P@ssw0rd");
        optionsBuilder.EnableDetailedErrors(true);
        optionsBuilder.UseLoggerFactory(_myLoggerFactory);
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditingSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("Auditing_Session");

            entity.Property(e => e.SessionId)
                .HasMaxLength(20)
                .HasColumnName("Session_ID");
            entity.Property(e => e.AttendanceEvidence)
                .HasColumnType("image")
                .HasColumnName("Attendance_Evidence");
            entity.Property(e => e.AttendanceType)
                .HasMaxLength(10)
                .HasColumnName("Attendance_Type");
            entity.Property(e => e.AuditorId)
                .HasMaxLength(20)
                .HasColumnName("Auditor_ID");
            entity.Property(e => e.AuditorName)
                .HasMaxLength(50)
                .HasColumnName("Auditor_Name");
            entity.Property(e => e.CenterId)
                .HasMaxLength(20)
                .HasColumnName("Center_ID");
            entity.Property(e => e.CenterName)
                .HasMaxLength(50)
                .HasColumnName("Center_Name");
            entity.Property(e => e.Conducted).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.ConnectionQuality)
                .HasMaxLength(10)
                .HasColumnName("Connection_Quality");
            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("Course_Name");
            entity.Property(e => e.CoursePlanWeekNumber)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Course_Plan_Week_Number");
            entity.Property(e => e.CurrentChapter)
                .HasMaxLength(10)
                .HasColumnName("Current_Chapter");
            entity.Property(e => e.EndTime).HasColumnName("End_Time");
            entity.Property(e => e.FilesDelivered)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("Files_Delivered");
            entity.Property(e => e.InstructorId)
                .HasMaxLength(20)
                .HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.MaterialDelivered)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("Material_Delivered");
            entity.Property(e => e.NumberAttended)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Attended");
            entity.Property(e => e.NumberRegistered)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Registered");
            entity.Property(e => e.PrevSessionLink)
                .HasMaxLength(50)
                .HasColumnName("Prev_Session_Link");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(20)
                .HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");
            entity.Property(e => e.SessionDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Session_DateTime");
            entity.Property(e => e.SessionType)
                .HasMaxLength(10)
                .HasColumnName("Session_Type");
            entity.Property(e => e.StartTime).HasColumnName("Start_Time");
            entity.Property(e => e.StudyGroupId)
                .HasMaxLength(20)
                .HasColumnName("Study_Group_ID");
            entity.Property(e => e.VideoQuality)
                .HasMaxLength(10)
                .HasColumnName("Video_Quality");
            entity.Property(e => e.VoiceQuality)
                .HasMaxLength(10)
                .HasColumnName("Voice_Quality");

            entity.HasOne(d => d.Auditor).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.AuditorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auditing_Session_Auditor");

            entity.HasOne(d => d.Center).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.CenterId)
                .HasConstraintName("FK_Auditing_Session_Training_Center");

            entity.HasOne(d => d.Course).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Auditing_Session_Course");

            entity.HasOne(d => d.Instructor).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Auditing_Session_Instructor");

            entity.HasOne(d => d.Provider).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK_Auditing_Session_Training_Provider");

            entity.HasOne(d => d.StudyGroup).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.StudyGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Auditing_Session_Study_Group");
        });

        modelBuilder.Entity<Auditor>(entity =>
        {
            entity.ToTable("Auditor");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.LinkedIn).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.SocialId)
                .HasMaxLength(20)
                .HasColumnName("Social_ID");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.ToTable("College");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Area).HasMaxLength(10);
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.IntId)
                .HasMaxLength(20)
                .HasColumnName("Int_ID");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfSections)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Sections");
            entity.Property(e => e.UniversityId)
                .HasMaxLength(20)
                .HasColumnName("University_ID");
            entity.Property(e => e.UniversityName)
                .HasMaxLength(50)
                .HasColumnName("University_Name");

            entity.HasOne(d => d.University).WithMany(p => p.Colleges)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_College_University");
        });

        modelBuilder.Entity<CollegeSection>(entity =>
        {
            entity.ToTable("College_Section");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Capacity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CollegeId)
                .HasMaxLength(20)
                .HasColumnName("College_ID");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(50)
                .HasColumnName("College_Name");
            entity.Property(e => e.IntId)
                .HasMaxLength(20)
                .HasColumnName("Int_ID");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");

            entity.HasOne(d => d.College).WithMany(p => p.CollegeSections)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK_College_Section_College");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .HasColumnName("Course_ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Capacity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Certification).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.InstructorId)
                .HasMaxLength(20)
                .HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.IntId)
                .HasMaxLength(20)
                .HasColumnName("Int_ID");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfExams)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Exams");
            entity.Property(e => e.NumberOfLabs)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_labs");
            entity.Property(e => e.NumberOfSessions)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Sessions");
            entity.Property(e => e.NumberOfStudyGroups)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Study_Groups");
            entity.Property(e => e.OnlineOfflineFlag)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Online_Offline_Flag");
            entity.Property(e => e.TrackId)
                .HasMaxLength(20)
                .HasColumnName("Track_ID");
            entity.Property(e => e.TrackName)
                .HasMaxLength(50)
                .HasColumnName("Track_Name");
        });

        modelBuilder.Entity<CoursePlan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Course_Plan");

            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("Course_Name");
            entity.Property(e => e.InstructorId)
                .HasMaxLength(20)
                .HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.Week1)
                .HasMaxLength(50)
                .HasColumnName("Week_1");
            entity.Property(e => e.Week10)
                .HasMaxLength(50)
                .HasColumnName("Week_10");
            entity.Property(e => e.Week11)
                .HasMaxLength(50)
                .HasColumnName("Week_11");
            entity.Property(e => e.Week12)
                .HasMaxLength(50)
                .HasColumnName("Week_12");
            entity.Property(e => e.Week13)
                .HasMaxLength(50)
                .HasColumnName("Week_13");
            entity.Property(e => e.Week14)
                .HasMaxLength(50)
                .HasColumnName("Week_14");
            entity.Property(e => e.Week15)
                .HasMaxLength(50)
                .HasColumnName("Week_15");
            entity.Property(e => e.Week16)
                .HasMaxLength(50)
                .HasColumnName("Week_16");
            entity.Property(e => e.Week2)
                .HasMaxLength(50)
                .HasColumnName("Week_2");
            entity.Property(e => e.Week3)
                .HasMaxLength(50)
                .HasColumnName("Week_3");
            entity.Property(e => e.Week4)
                .HasMaxLength(50)
                .HasColumnName("Week_4");
            entity.Property(e => e.Week5)
                .HasMaxLength(50)
                .HasColumnName("Week_5");
            entity.Property(e => e.Week6)
                .HasMaxLength(50)
                .HasColumnName("Week_6");
            entity.Property(e => e.Week7)
                .HasMaxLength(50)
                .HasColumnName("Week_7");
            entity.Property(e => e.Week8)
                .HasMaxLength(50)
                .HasColumnName("Week_8");
            entity.Property(e => e.Week9)
                .HasMaxLength(50)
                .HasColumnName("Week_9");

            //entity.HasOne(d => d.Course).WithMany()
            //    .HasForeignKey(d => d.CourseId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Course_Plan_Course");

            //entity.HasOne(d => d.Instructor).WithMany()
            //    .HasForeignKey(d => d.InstructorId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Course_Plan_Instructor");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.ToTable("Instructor");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Certificate).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CollegeId)
                .HasMaxLength(20)
                .HasColumnName("College_ID");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(50)
                .HasColumnName("College_Name");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.Grade).HasMaxLength(10);
            entity.Property(e => e.GraduationDate).HasColumnName("Graduation_Date");
            entity.Property(e => e.LinkedIn).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.SocialId)
                .HasMaxLength(20)
                .HasColumnName("Social_ID");
            entity.Property(e => e.UniversityId)
                .HasMaxLength(20)
                .HasColumnName("University_ID");
            entity.Property(e => e.UniversityName)
                .HasMaxLength(50)
                .HasColumnName("University_Name");
        });

        modelBuilder.Entity<ProviderCenter>(entity =>
        {
            entity
                .HasKey(e => new { e.ProviderId, e.CenterId });

            entity.ToTable("Provider_Center");

            entity.Property(e => e.CenterId)
                .HasMaxLength(20)
                .HasColumnName("Center_ID");
            entity.Property(e => e.CenterName)
                .HasMaxLength(50)
                .HasColumnName("Center_Name");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(20)
                .HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");

            entity.HasOne(d => d.Center).WithMany(d=>d.ProviderCenters)
                .HasForeignKey(d => d.CenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provider_Center_Training_Center");

            entity.HasOne(d => d.Provider).WithMany(d=>d.ProviderCenters)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provider_Center_Training_Provider");
        });

        //modelBuilder.Entity<ProviderStudyGroup>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToTable("Provider_Study_Group");

        //    entity.Property(e => e.ProviderId)
        //        .HasMaxLength(20)
        //        .HasColumnName("Provider_ID");
        //    entity.Property(e => e.Remarks).HasMaxLength(100);
        //    entity.Property(e => e.StudyGroupId)
        //        .HasMaxLength(20)
        //        .HasColumnName("Study_Group_ID");
        //    entity.Property(e => e.YearSemester)
        //        .HasColumnType("numeric(18, 0)")
        //        .HasColumnName("Year_Semester");

        //    //entity.HasOne(d => d.Provider).WithMany()
        //    //    .HasForeignKey(d => d.ProviderId)
        //    //    .HasConstraintName("FK_Provider_Study_Group_Training_Provider");

        //    //entity.HasOne(d => d.StudyGroup).WithMany()
        //    //    .HasForeignKey(d => d.StudyGroupId)
        //    //    .HasConstraintName("FK_Provider_Study_Group_Study_Group");
        //});

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.HasKey(e => e.StudentIntId);

            entity.Property(e => e.StudentIntId)
                .HasMaxLength(20)
                .HasColumnName("Student_INT_ID");
            entity.Property(e => e.StudentAppId)
                .HasMaxLength(20)
                .HasColumnName("Student_App_ID");

            
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(10);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(20);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.LinkedIn).HasMaxLength(50);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.Nationality).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.SocialId)
                .HasMaxLength(20)
                .HasColumnName("Social_ID");
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.StudyGovernorate)
                .HasMaxLength(10)
                .HasColumnName("Study_Governorate");
            entity.Property(e => e.Round_Code)
                .HasMaxLength(20)
                .HasColumnName("Round_Code");

            entity.Property(e => e.TrackId)
                .HasMaxLength(50)
                .HasColumnName("Track_ID");

            entity.Property(e => e.Active)
                .HasColumnName("Active");

            entity.HasOne(d => d.StudyGroup).WithMany(p => p.Students)
                .HasForeignKey(d => d.Round_Code)
                .HasConstraintName("FK_Student_Study_Group");

            entity.HasOne(d => d.Track).WithMany(p => p.Students)
                .HasForeignKey(d => d.TrackId);
                //.HasConstraintName("FK_Student_Track");
        });

        //modelBuilder.Entity<StudentTest>(entity =>
        //{
        //    entity.ToTable("StudentTest");

        //    entity.Property(e => e.Id)
        //        .HasMaxLength(10)
        //        .IsFixedLength()
        //        .HasColumnName("ID");
        //    entity.Property(e => e.Name)
        //        .HasMaxLength(10)
        //        .IsFixedLength();
        //    entity.Property(e => e.NameAr)
        //        .HasMaxLength(10)
        //        .IsFixedLength();
        //});

        modelBuilder.Entity<StudyGroup>(entity =>
        {
            entity.HasKey(e => e.RoundCode);

            entity.ToTable("Study_Group");

            entity.Property(e => e.RoundCode)
                .HasMaxLength(50)
                .HasColumnName("Round_Code");
            entity.Property(e => e.Capacity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CourseId)
                .HasMaxLength(20)
                .HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("Course_Name");
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.InstructorId)
                .HasMaxLength(20)
                .HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.IntId)
                .HasMaxLength(20)
                .HasColumnName("Int_ID");
            entity.Property(e => e.NumberOfStudents)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Students");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(20)
                .HasColumnName("Provider_ID");
            entity.Property(e => e.StudyGroupType)
                .HasMaxLength(10)
                .HasColumnName("Study_Group_Type");
            entity.Property(e => e.TrackId)
                .HasMaxLength(20)
                .HasColumnName("Track_ID");
            entity.Property(e => e.YearSemester)
                .HasMaxLength(10)
                .HasColumnName("Year_Semester");

            entity.HasOne(d => d.Instructor).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Study_Group_Instructor");

            entity.HasOne(d => d.Track).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Study_Group_Track");

            entity.HasMany(d => d.Students)
            .WithOne(d => d.StudyGroup)
            .HasForeignKey(d => d.Round_Code);
        });

        modelBuilder.Entity<StudyGroupSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("Study_Group_Session");

            entity.Property(e => e.SessionId)
                .HasMaxLength(20)
                .HasColumnName("Session_ID");
            entity.Property(e => e.ConnectionQuality)
                .HasMaxLength(10)
                .HasColumnName("Connection_Quality");
            entity.Property(e => e.InstructorId)
                .HasMaxLength(20)
                .HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.MaterialDelivered)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("Material_Delivered");
            entity.Property(e => e.SessionLink)
                .HasMaxLength(50)
                .HasColumnName("Session_Link");
            entity.Property(e => e.SessionNumber)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Session_Number");
            entity.Property(e => e.StudyGroupId)
                .HasMaxLength(20)
                .HasColumnName("Study_Group_ID");
            entity.Property(e => e.VideoQuality)
                .HasMaxLength(10)
                .HasColumnName("Video_Quality");
            entity.Property(e => e.VoiceQuality)
                .HasMaxLength(10)
                .HasColumnName("Voice_Quality");

            entity.HasOne(d => d.Instructor).WithMany(p => p.StudyGroupSessions)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Study_Group_Session_Instructor");

            entity.HasOne(d => d.StudyGroup).WithMany(p => p.StudyGroupSessions)
                .HasForeignKey(d => d.StudyGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Study_Group_Session_Study_Group");
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.ToTable("Track");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Category).HasMaxLength(10);
            entity.Property(e => e.Duration).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfCourses)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Courses");
            entity.Property(e => e.ProviderId)
                .HasMaxLength(20)
                .HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");
            entity.HasMany(e => e.Students)
            .WithOne(e => e.Track)
            .HasPrincipalKey(e => e.Track_Code);
        });

        modelBuilder.Entity<TrainingCenter>(entity =>
        {
            entity.ToTable("Training_Center");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Area).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfClassrooms)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Classrooms");
            entity.Property(e => e.NumberOfLabs)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Labs");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Website).HasMaxLength(50);
        });

        modelBuilder.Entity<TrainingProvider>(entity =>
        {
            entity.ToTable("Training_Provider");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Area).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfTracks)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Tracks");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Website).HasMaxLength(50);
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.ToTable("University");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("ID");
            entity.Property(e => e.Active).HasColumnType("numeric(1, 0)");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Area).HasMaxLength(10);
            entity.Property(e => e.Governorate).HasMaxLength(10);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfColleges)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Colleges");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<AuditorRoundCodeAssignment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Auditor_Round_Code_Assignment");

            entity.HasIndex(e => e.AuditorId, "IX_Auditor_Sessions");

            entity.Property(e => e.AuditorId).HasColumnName("Auditor_ID");
            entity.Property(e => e.StudyGroupRoundCode)
                .HasMaxLength(50)
                .HasColumnName("Study_Group_Round_Code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
