using System;
using System.Collections.Generic;
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

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GBPLSPS;Database=StudentDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;User ID=sherin;Password=P@ssw0rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<AuditorRoundCodeAssignment>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToTable("Auditor_Round_Code_Assignment");

        //    entity.HasIndex(e => e.AuditorId, "IX_Auditor_Sessions");

        //    entity.Property(e => e.AuditorId).HasColumnName("Auditor_ID");
        //    entity.Property(e => e.StudyGroupRoundCode)
        //        .HasMaxLength(50)
        //        .HasColumnName("Study_Group_Round_Code");
        //});

        //OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
