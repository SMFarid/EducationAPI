using System;
using System.Collections.Generic;
using EducationAPI;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Context;

public partial class StudentTest : DbContext
{
    public StudentTest()
    {
    }

    public StudentTest(DbContextOptions<StudentTest> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditingSession> AuditingSessions { get; set; }

    public virtual DbSet<Auditor> Auditors { get; set; }

    public virtual DbSet<AuditorRoundCodeAssignment> AuditorRoundCodeAssignments { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<CollegeSection> CollegeSections { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseInstanceDet> CourseInstanceDets { get; set; }

    public virtual DbSet<CourseInstanceHdr> CourseInstanceHdrs { get; set; }

    public virtual DbSet<GroupCourseDet> GroupCourseDets { get; set; }

    public virtual DbSet<GroupSession> GroupSessions { get; set; }

    public virtual DbSet<GroupTrackDet> GroupTrackDets { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<JobProfile> JobProfiles { get; set; }

    public virtual DbSet<JobProfileCourseHdr> JobProfileCourseHdrs { get; set; }

    public virtual DbSet<ProviderCenter> ProviderCenters { get; set; }

    public virtual DbSet<ProviderStudyGroup> ProviderStudyGroups { get; set; }

    public virtual DbSet<SessionInstance> SessionInstances { get; set; }

    public virtual DbSet<StudyGroup> StudyGroups { get; set; }

    public virtual DbSet<StudyGroupDay> StudyGroupDays { get; set; }

    public virtual DbSet<StudyGroupSession> StudyGroupSessions { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<TrackProvider> TrackProviders { get; set; }

    public virtual DbSet<Trainee> Trainees { get; set; }

    public virtual DbSet<TraineeGroupCourse> TraineeGroupCourses { get; set; }

    public virtual DbSet<TrainingCenter> TrainingCenters { get; set; }

    public virtual DbSet<TrainingProvider> TrainingProviders { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=CALIBARN;Database=DEPI;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;User ID=sherin;Password=P@ssw0rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditingSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("Auditing_Session");

            entity.Property(e => e.SessionId).HasColumnName("Session_ID");
            entity.Property(e => e.AttendanceEvidence)
                .HasColumnType("image")
                .HasColumnName("Attendance_Evidence");
            entity.Property(e => e.AttendanceType)
                .HasMaxLength(50)
                .HasColumnName("Attendance_Type");
            entity.Property(e => e.AuditorId).HasColumnName("Auditor_ID");
            entity.Property(e => e.AuditorName)
                .HasMaxLength(100)
                .HasColumnName("Auditor_Name");
            entity.Property(e => e.CenterId).HasColumnName("Center_ID");
            entity.Property(e => e.CenterName)
                .HasMaxLength(100)
                .HasColumnName("Center_Name");
            entity.Property(e => e.ConnectionQuality)
                .HasMaxLength(50)
                .HasColumnName("Connection_Quality");
            entity.Property(e => e.CourseId)
                .HasMaxLength(50)
                .HasColumnName("Course_ID");
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .HasColumnName("Course_Name");
            entity.Property(e => e.CoursePlanWeekNumber)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Course_Plan_Week_Number");
            entity.Property(e => e.CurrentChapter)
                .HasMaxLength(50)
                .HasColumnName("Current_Chapter");
            entity.Property(e => e.EndTime).HasColumnName("End_Time");
            entity.Property(e => e.FilesDelivered).HasColumnName("Files_Delivered");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(100)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.LabTestFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("Lab_Test_Flag");
            entity.Property(e => e.MaterialDelivered).HasColumnName("Material_Delivered");
            entity.Property(e => e.NumberAttended)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Attended");
            entity.Property(e => e.NumberRegistered)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Registered");
            entity.Property(e => e.PrevSessionLink)
                .HasMaxLength(100)
                .HasColumnName("Prev_Session_Link");
            entity.Property(e => e.ProviderId).HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");
            entity.Property(e => e.SessionDateTimeClose)
                .HasColumnType("datetime")
                .HasColumnName("Session_DateTime_Close");
            entity.Property(e => e.SessionDateTimeStart)
                .HasColumnType("datetime")
                .HasColumnName("Session_DateTime_Start");
            entity.Property(e => e.SessionType)
                .HasMaxLength(50)
                .HasColumnName("Session_Type");
            entity.Property(e => e.StartTime).HasColumnName("Start_Time");
            entity.Property(e => e.StudyGroupId)
                .HasMaxLength(50)
                .HasColumnName("Study_Group_ID");
            entity.Property(e => e.VideoQuality)
                .HasMaxLength(50)
                .HasColumnName("Video_Quality");
            entity.Property(e => e.VoiceQuality)
                .HasMaxLength(50)
                .HasColumnName("Voice_Quality");

            entity.HasOne(d => d.Auditor).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.AuditorId)
                .HasConstraintName("FK_Auditing_Session_Auditor");

            entity.HasOne(d => d.Center).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.CenterId)
                .HasConstraintName("FK_Auditing_Session_Training_Center");

            entity.HasOne(d => d.Instructor).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Auditing_Session_Instructor");

            entity.HasOne(d => d.Provider).WithMany(p => p.AuditingSessions)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK_Auditing_Session_Training_Provider");
        });

        modelBuilder.Entity<Auditor>(entity =>
        {
            entity.ToTable("Auditor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(1);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.LinkedIn).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.NameAr)
                .HasMaxLength(100)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_En");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.SocialId)
                .HasMaxLength(50)
                .HasColumnName("Social_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<AuditorRoundCodeAssignment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Auditor_Round_Code_Assignment");

            entity.Property(e => e.AuditingSessionId).HasColumnName("Auditing_Session_ID");
            entity.Property(e => e.AuditorId).HasColumnName("Auditor_ID");
            entity.Property(e => e.StudyGroupRoundCode)
                .HasMaxLength(50)
                .HasColumnName("Study_Group_Round_Code");

            entity.HasOne(d => d.Auditor).WithMany()
                .HasForeignKey(d => d.AuditorId)
                .HasConstraintName("FK_Auditor_Sessions_Auditor");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.ToTable("College");

            entity.Property(e => e.CollegeId)
                .ValueGeneratedNever()
                .HasColumnName("College_ID");
            entity.Property(e => e.Area).HasMaxLength(50);
            entity.Property(e => e.CollegeCode)
                .HasMaxLength(50)
                .HasColumnName("College_Code");
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfSections).HasColumnName("Number_Of_Sections");
            entity.Property(e => e.UniversityId).HasColumnName("University_ID");
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

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CollegeId).HasColumnName("College_ID");
            entity.Property(e => e.CollegeName)
                .HasMaxLength(50)
                .HasColumnName("College_Name");
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.SectionCode)
                .HasMaxLength(50)
                .HasColumnName("Section_Code");

            entity.HasOne(d => d.College).WithMany(p => p.CollegeSections)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK_College_Section_College");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseIntId);

            entity.ToTable("Course");

            entity.Property(e => e.CourseIntId).HasColumnName("Course_Int_ID");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("Course_Code");
            entity.Property(e => e.NameAr)
                .HasMaxLength(200)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(200)
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
        });

        modelBuilder.Entity<CourseInstanceDet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Course_Instance_Det");

            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("datetime")
                .HasColumnName("Active_To");
            entity.Property(e => e.CenterId).HasColumnName("Center_ID");
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.CourseInstanceHdrId).HasColumnName("Course_Instance_Hdr_ID");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.ProviderId).HasColumnName("Provider_ID");

            entity.HasOne(d => d.CourseInstanceHdr).WithMany()
                .HasForeignKey(d => d.CourseInstanceHdrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Instance_Det_Course_Instance_Hdr");
        });

        modelBuilder.Entity<CourseInstanceHdr>(entity =>
        {
            entity.ToTable("Course_Instance_Hdr");

            entity.Property(e => e.CourseInstanceHdrId).HasColumnName("Course_Instance_Hdr_ID");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .HasColumnName("Course_Code");
            entity.Property(e => e.CourseIntId).HasColumnName("Course_Int_ID");
            entity.Property(e => e.ExpectedEndDate)
                .HasColumnType("datetime")
                .HasColumnName("Expected_End_Date");
            entity.Property(e => e.JobProfileIntId).HasColumnName("Job_Profile_Int_ID");
            entity.Property(e => e.NumberOfExams).HasColumnName("Number_Of_Exams");
            entity.Property(e => e.NumberOfLabs).HasColumnName("Number_Of_Labs");
            entity.Property(e => e.NumberOfSessions).HasColumnName("Number_Of_Sessions");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.TrackId).HasColumnName("Track_ID");

            entity.HasOne(d => d.CourseInt).WithMany(p => p.CourseInstanceHdrs)
                .HasForeignKey(d => d.CourseIntId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Course_Instance_Hdr_Course");

            entity.HasOne(d => d.JobProfileInt).WithMany(p => p.CourseInstanceHdrs)
                .HasForeignKey(d => d.JobProfileIntId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Course_Instance_Hdr_Job_Profile");

            entity.HasOne(d => d.Track).WithMany(p => p.CourseInstanceHdrs)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Course_Instance_Hdr_Track");
        });

        modelBuilder.Entity<GroupCourseDet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Group_Course_Det");

            entity.Property(e => e.CourseIntId).HasColumnName("Course_Int_ID");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");

            entity.HasOne(d => d.CourseInt).WithMany()
                .HasForeignKey(d => d.CourseIntId)
                .HasConstraintName("FK_Group_Course_Det_Course");

            entity.HasOne(d => d.GroupInt).WithMany()
                .HasForeignKey(d => d.GroupIntId)
                .HasConstraintName("FK_Group_Course_Det_Study_Group");
        });

        modelBuilder.Entity<GroupSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("Group_Session");

            entity.Property(e => e.SessionId).HasColumnName("Session_ID");
            entity.Property(e => e.CourseIntId).HasColumnName("Course_Int_ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.SessionStatus).HasColumnName("Session_Status");
            entity.Property(e => e.SessionTime).HasColumnName("Session_Time");
            entity.Property(e => e.StudyGroupIntId).HasColumnName("Study_Group_Int_ID");

            entity.HasOne(d => d.CourseInt).WithMany(p => p.GroupSessions)
                .HasForeignKey(d => d.CourseIntId)
                .HasConstraintName("FK_Group_Session_Group_Session");

            entity.HasOne(d => d.Instructor).WithMany(p => p.GroupSessions)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Group_Session_Instructor");

            entity.HasOne(d => d.StudyGroupInt).WithMany(p => p.GroupSessions)
                .HasForeignKey(d => d.StudyGroupIntId)
                .HasConstraintName("FK_Group_Session_Study_Group");
        });

        modelBuilder.Entity<GroupTrackDet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Group_Track_Det");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.TrackIntId).HasColumnName("Track_Int_ID");

            entity.HasOne(d => d.GroupInt).WithMany()
                .HasForeignKey(d => d.GroupIntId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Group_Track_Det_Study_Group");

            entity.HasOne(d => d.TrackInt).WithMany()
                .HasForeignKey(d => d.TrackIntId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Group_Track_Det_Track");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorIntId);

            entity.ToTable("Instructor");

            entity.Property(e => e.InstructorIntId).HasColumnName("Instructor_Int_ID");
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

        modelBuilder.Entity<JobProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileIntId);

            entity.ToTable("Job_Profile");

            entity.Property(e => e.ProfileIntId).HasColumnName("Profile_Int_ID");
            entity.Property(e => e.ActiveFrom)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Active_To");
            entity.Property(e => e.NameAr)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Name_En");
            entity.Property(e => e.ProfileId)
                .HasMaxLength(50)
                .HasColumnName("Profile_ID");
            entity.Property(e => e.TrackIntId).HasColumnName("Track_Int_ID");

            entity.HasOne(d => d.TrackInt).WithMany(p => p.JobProfiles)
                .HasForeignKey(d => d.TrackIntId)
                .HasConstraintName("FK_Job_Profile_Track");
        });

        modelBuilder.Entity<JobProfileCourseHdr>(entity =>
        {
            entity.HasKey(e => e.IntId);

            entity.ToTable("JobProfile_Course_Hdr");

            entity.Property(e => e.IntId).HasColumnName("Int_ID");
            entity.Property(e => e.ActiveFrom)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Active_To");
            entity.Property(e => e.CourseIntId).HasColumnName("Course_Int_ID");
            entity.Property(e => e.JobProfileIntId).HasColumnName("Job_Profile_Int_ID");

            entity.HasOne(d => d.CourseInt).WithMany(p => p.JobProfileCourseHdrs)
                .HasForeignKey(d => d.CourseIntId)
                .HasConstraintName("FK_JobProfile_Course_Hdr_Course");

            entity.HasOne(d => d.JobProfileInt).WithMany(p => p.JobProfileCourseHdrs)
                .HasForeignKey(d => d.JobProfileIntId)
                .HasConstraintName("FK_JobProfile_Course_Hdr_Job_Profile");
        });

        modelBuilder.Entity<ProviderCenter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Provider_Center");

            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("datetime")
                .HasColumnName("Active_To");
            entity.Property(e => e.CenterId).HasColumnName("Center_ID");
            entity.Property(e => e.CenterName)
                .HasMaxLength(50)
                .HasColumnName("Center_Name");
            entity.Property(e => e.ProviderId).HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(50)
                .HasColumnName("Provider_Name");

            entity.HasOne(d => d.Center).WithMany()
                .HasForeignKey(d => d.CenterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provider_Center_Training_Center");

            entity.HasOne(d => d.Provider).WithMany()
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Provider_Center_Training_Provider");
        });

        modelBuilder.Entity<ProviderStudyGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Provider_Study_Group");

            entity.Property(e => e.ProviderId).HasColumnName("Provider_ID");
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.StudyGroupIntId).HasColumnName("Study_Group_Int_ID");
            entity.Property(e => e.YearSemester)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Year_Semester");

            entity.HasOne(d => d.Provider).WithMany()
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK_Provider_Study_Group_Training_Provider");

            entity.HasOne(d => d.StudyGroupInt).WithMany()
                .HasForeignKey(d => d.StudyGroupIntId)
                .HasConstraintName("FK_Provider_Study_Group_Study_Group");
        });

        modelBuilder.Entity<SessionInstance>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Session_Instance");

            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.SessionDate)
                .HasColumnType("datetime")
                .HasColumnName("Session_Date");
            entity.Property(e => e.SessionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Session_ID");
            entity.Property(e => e.SessionRecording)
                .HasMaxLength(50)
                .HasColumnName("Session_Recording");

            entity.HasOne(d => d.Course).WithMany()
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Session_Instance_Course");

            entity.HasOne(d => d.GroupInt).WithMany()
                .HasForeignKey(d => d.GroupIntId)
                .HasConstraintName("FK_Session_Instance_Study_Group");

            entity.HasOne(d => d.Session).WithMany()
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Instance_Group_Session");
        });

        modelBuilder.Entity<StudyGroup>(entity =>
        {
            entity.HasKey(e => e.GroupIntId);

            entity.ToTable("Study_Group");

            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.Capacity).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.ExpectedEndTime)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Expected_End_Time");
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.GroupStartTime)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Group_Start_Time");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.JobProfileIntId).HasColumnName("Job_Profile_Int_ID");
            entity.Property(e => e.LocationAddress)
                .HasMaxLength(300)
                .HasColumnName("Location_Address");
            entity.Property(e => e.LocationGoogleMap)
                .HasMaxLength(100)
                .HasColumnName("Location_Google_Map");
            entity.Property(e => e.MeetingLink)
                .HasMaxLength(100)
                .HasColumnName("Meeting_Link");
            entity.Property(e => e.MeetingLinkId)
                .HasMaxLength(100)
                .HasColumnName("Meeting_Link_ID");
            entity.Property(e => e.MeetingLinkPasscode)
                .HasMaxLength(50)
                .HasColumnName("Meeting_Link_Passcode");
            entity.Property(e => e.NumberOfStudents)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Students");
            entity.Property(e => e.RoundCode)
                .HasMaxLength(50)
                .HasColumnName("Round_Code");
            entity.Property(e => e.StudyGroupType)
                .HasMaxLength(50)
                .HasColumnName("Study_Group_Type");
            entity.Property(e => e.TrackCode)
                .HasMaxLength(50)
                .HasColumnName("Track_Code");
            entity.Property(e => e.TrackIntId).HasColumnName("Track_Int_ID");
            entity.Property(e => e.TraineeType)
                .HasMaxLength(1)
                .HasColumnName("Trainee_Type");
            entity.Property(e => e.WeekDayEndFlag)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Week_Day_End_Flag");
            entity.Property(e => e.WelcomeMessage).HasColumnName("Welcome_Message");
            entity.Property(e => e.YearSemester)
                .HasMaxLength(50)
                .HasColumnName("Year_Semester");

            entity.HasOne(d => d.Instructor).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Study_Group_Instructor");

            entity.HasOne(d => d.JobProfileInt).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.JobProfileIntId)
                .HasConstraintName("FK_Study_Group_Job_Profile");

            entity.HasOne(d => d.TrackInt).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.TrackIntId)
                .HasConstraintName("FK_Study_Group_Track");

            entity.HasOne(d => d.TrackIntNavigation).WithMany(p => p.StudyGroups)
                .HasForeignKey(d => d.TrackIntId)
                .HasConstraintName("FK_Study_Group_Track1");
        });

        modelBuilder.Entity<StudyGroupDay>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Study_Group_Days");

            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("datetime")
                .HasColumnName("Active_To");
            entity.Property(e => e.CoachingDay)
                .HasMaxLength(50)
                .HasColumnName("Coaching_Day");
            entity.Property(e => e.EnglishDay)
                .HasMaxLength(50)
                .HasColumnName("English_Day");
            entity.Property(e => e.OnlineDay1)
                .HasMaxLength(50)
                .HasColumnName("Online_Day_1");
            entity.Property(e => e.OnlineDay2)
                .HasMaxLength(50)
                .HasColumnName("Online_Day_2");
            entity.Property(e => e.OnlineDay3)
                .HasMaxLength(50)
                .HasColumnName("Online_Day_3");
            entity.Property(e => e.PhysicalDay)
                .HasMaxLength(50)
                .HasColumnName("Physical_Day");
            entity.Property(e => e.SoftskillDay)
                .HasMaxLength(50)
                .HasColumnName("Softskill_Day");
            entity.Property(e => e.StudyGroupId).HasColumnName("Study_Group_ID");

            entity.HasOne(d => d.StudyGroup).WithMany()
                .HasForeignKey(d => d.StudyGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Study_Group_Days_Study_Group");
        });

        modelBuilder.Entity<StudyGroupSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("Study_Group_Session");

            entity.Property(e => e.SessionId).HasColumnName("Session_ID");
            entity.Property(e => e.ConnectionQuality).HasColumnName("Connection_Quality");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.InstructorId).HasColumnName("Instructor_ID");
            entity.Property(e => e.InstructorName)
                .HasMaxLength(50)
                .HasColumnName("Instructor_Name");
            entity.Property(e => e.MaterialDelivered).HasColumnName("Material_Delivered");
            entity.Property(e => e.SessionLink)
                .HasMaxLength(100)
                .HasColumnName("Session_Link");
            entity.Property(e => e.SessionNumber).HasColumnName("Session_Number");
            entity.Property(e => e.StudyGroupId).HasColumnName("Study_Group_ID");
            entity.Property(e => e.TimeInterval).HasColumnName("Time_Interval");
            entity.Property(e => e.VideoQuality).HasColumnName("Video_Quality");
            entity.Property(e => e.VoiceQuality).HasColumnName("Voice_Quality");

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
            entity.HasKey(e => e.TrackIntId).HasName("PK_Track_1");

            entity.ToTable("Track");

            entity.Property(e => e.TrackIntId).HasColumnName("Track_Int_ID");
            entity.Property(e => e.TrackCode)
                .HasMaxLength(50)
                .HasColumnName("Track_Code");
            entity.Property(e => e.TrackNameAr)
                .HasMaxLength(50)
                .HasColumnName("Track_Name_Ar");
            entity.Property(e => e.TrackNameEn)
                .HasMaxLength(50)
                .HasColumnName("Track_Name_En");
        });

        modelBuilder.Entity<TrackProvider>(entity =>
        {
            entity.HasKey(e => e.IntId).HasName("PK_Track");

            entity.ToTable("Track_Provider");

            entity.Property(e => e.IntId).HasColumnName("Int_ID");
            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("datetime")
                .HasColumnName("Active_To");
            entity.Property(e => e.NameAr)
                .HasMaxLength(200)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(200)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfCourses).HasColumnName("Number_Of_Courses");
            entity.Property(e => e.ProviderId).HasColumnName("Provider_ID");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(200)
                .HasColumnName("Provider_Name");
            entity.Property(e => e.TrackDuration).HasColumnName("Track_Duration");
            entity.Property(e => e.TrackIntId).HasColumnName("Track_Int_ID");
            entity.Property(e => e.TrackLeader)
                .HasMaxLength(100)
                .HasColumnName("Track_Leader");

            entity.HasOne(d => d.TrackInt).WithMany(p => p.TrackProviders)
                .HasForeignKey(d => d.TrackIntId)
                .HasConstraintName("FK_Track_Provider_Training_Provider");
        });

        modelBuilder.Entity<Trainee>(entity =>
        {
            entity.HasKey(e => e.TraineeIntId).HasName("PK_Student");

            entity.ToTable("Trainee");

            entity.Property(e => e.TraineeIntId).HasColumnName("Trainee_INT_ID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.Governorate).HasMaxLength(100);
            entity.Property(e => e.LinkedIn).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_En");
            entity.Property(e => e.Nationality).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.RoundCode)
                .HasMaxLength(50)
                .HasColumnName("Round_Code");
            entity.Property(e => e.SocialId)
                .HasMaxLength(100)
                .HasColumnName("Social_ID");
            entity.Property(e => e.Status).HasMaxLength(100);
            entity.Property(e => e.StudentAppId)
                .HasMaxLength(100)
                .HasColumnName("Student_App_ID");
            entity.Property(e => e.StudyGovernorate)
                .HasMaxLength(50)
                .HasColumnName("Study_Governorate");
            entity.Property(e => e.TrackId).HasColumnName("Track_ID");

            entity.HasOne(d => d.Track).WithMany(p => p.Trainees)
                .HasForeignKey(d => d.TrackId)
                .HasConstraintName("FK_Trainee_Track");
        });

        modelBuilder.Entity<TraineeGroupCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Trainee_Group_Course");

            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("Active_From");
            entity.Property(e => e.ActiveTo)
                .HasColumnType("datetime")
                .HasColumnName("Active_To");
            entity.Property(e => e.CourseId).HasColumnName("Course_ID");
            entity.Property(e => e.GroupIntId).HasColumnName("Group_Int_ID");
            entity.Property(e => e.RoundCode)
                .HasMaxLength(50)
                .HasColumnName("Round_Code");
            entity.Property(e => e.TraineeId).HasColumnName("Trainee_ID");

            entity.HasOne(d => d.Course).WithMany()
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Student_Group_Course_Course_Instance_Hdr");

            entity.HasOne(d => d.GroupInt).WithMany()
                .HasForeignKey(d => d.GroupIntId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Group_Course_Study_Group");

            entity.HasOne(d => d.Trainee).WithMany()
                .HasForeignKey(d => d.TraineeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Group_Course_Student");
        });

        modelBuilder.Entity<TrainingCenter>(entity =>
        {
            entity.HasKey(e => e.CenterIntId);

            entity.ToTable("Training_Center");

            entity.Property(e => e.CenterIntId).HasColumnName("Center_Int_ID");
            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.CenterCode)
                .HasMaxLength(50)
                .HasColumnName("Center_Code");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.NameAr)
                .HasMaxLength(100)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfClassrooms)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Classrooms");
            entity.Property(e => e.NumberOfLabs)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("Number_Of_Labs");
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Website).HasMaxLength(100);
        });

        modelBuilder.Entity<TrainingProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderIntId);

            entity.ToTable("Training_Provider");

            entity.Property(e => e.ProviderIntId).HasColumnName("Provider_Int_ID");
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Area).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.NameAr)
                .HasMaxLength(100)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(100)
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

            entity.Property(e => e.UniversityId)
                .ValueGeneratedNever()
                .HasColumnName("University_ID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Area).HasMaxLength(100);
            entity.Property(e => e.Governorate).HasMaxLength(50);
            entity.Property(e => e.NameAr)
                .HasMaxLength(50)
                .HasColumnName("Name_Ar");
            entity.Property(e => e.NameEn)
                .HasMaxLength(50)
                .HasColumnName("Name_En");
            entity.Property(e => e.NumberOfColleges).HasColumnName("Number_Of_Colleges");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
