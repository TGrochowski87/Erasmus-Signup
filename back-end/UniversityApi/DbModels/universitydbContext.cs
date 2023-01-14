using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniversityApi.DbModels;

public partial class UniversitydbContext : DbContext
{
    public UniversitydbContext()
    {
    }

    public UniversitydbContext(DbContextOptions<UniversitydbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContractDetail> ContractDetails { get; set; }

    public virtual DbSet<DestSpeciality> DestSpecialities { get; set; }

    public virtual DbSet<MinGradeHistory> MinGradeHistories { get; set; }

    public virtual DbSet<PwrFaculty> PwrFaculties { get; set; }

    public virtual DbSet<PwrSpeciality> PwrSpecialities { get; set; }

    public virtual DbSet<PwrSubject> PwrSubjects { get; set; }

    public virtual DbSet<StudyArea> StudyAreas { get; set; }

    public virtual DbSet<StudyDomain> StudyDomains { get; set; }

    public virtual DbSet<SubjectLanguage> SubjectLanguages { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=erasmus-db.postgres.database.azure.com:5432;Database=university;Username=postgres;Password=Erasmus123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("t_pwr_fac", new[] { "Architecture", "Civil Engineering", "Chemistry", "Information and Communication Technology", "Electrical Engineering", "Geoengineering", " Mining and Geology", "Environmental Engineering", "Management", "Mechanical and Power Engineering", "Mechanical Engineering", "Fundamental Problems of Technology", "Electronics", " Photonics and Microsystems", "Pure and Applied Mathematics", "University-wide", "Off-campus Department WUST" })
            .HasPostgresEnum("t_pwr_fac_sh", new[] { "PWr", "W1", "W2", "W3", "W4N", "W5", "W6", "W7", "W8N", "W9", "W10", "W11", "W12N", "W13", "W15", "F3" });

        modelBuilder.Entity<ContractDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contract_details_pkey");

            entity.ToTable("contract_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcceptingDoctoral).HasColumnName("accepting_doctoral");
            entity.Property(e => e.AcceptingPostgraduate).HasColumnName("accepting_postgraduate");
            entity.Property(e => e.AcceptingUndergraduate).HasColumnName("accepting_undergraduate");
            entity.Property(e => e.ConclusionDate).HasColumnName("conclusion_date");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.VacancyMaxPositions).HasColumnName("vacancy_max_positions");
            entity.Property(e => e.VacancyMonths).HasColumnName("vacancy_months");
        });

        modelBuilder.Entity<DestSpeciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dest_speciality_pkey");

            entity.ToTable("dest_speciality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractDetailsId).HasColumnName("contract_details_id");
            entity.Property(e => e.DestUniversityCode)
                .HasMaxLength(30)
                .HasColumnName("dest_university_code");
            entity.Property(e => e.InterestedStudents).HasColumnName("interested_students");
            entity.Property(e => e.StudyAreaId)
                .HasMaxLength(4)
                .HasColumnName("study_area_id");
            entity.Property(e => e.SubjectLanguageId).HasColumnName("subject_language_id");

            entity.HasOne(d => d.ContractDetails).WithMany(p => p.DestSpecialities)
                .HasForeignKey(d => d.ContractDetailsId)
                .HasConstraintName("dest_speciality_contract_details_id_fkey");

            entity.HasOne(d => d.DestUniversityCodeNavigation).WithMany(p => p.DestSpecialities)
                .HasForeignKey(d => d.DestUniversityCode)
                .HasConstraintName("dest_speciality_dest_university_code_fkey");

            entity.HasOne(d => d.StudyArea).WithMany(p => p.DestSpecialities)
                .HasForeignKey(d => d.StudyAreaId)
                .HasConstraintName("dest_speciality_study_area_id_fkey");

            entity.HasOne(d => d.SubjectLanguage).WithMany(p => p.DestSpecialities)
                .HasForeignKey(d => d.SubjectLanguageId)
                .HasConstraintName("dest_speciality_subject_language_id_fkey");
        });

        modelBuilder.Entity<MinGradeHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("min_grade_history_pkey");

            entity.ToTable("min_grade_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DestSpecialityId).HasColumnName("dest_speciality_id");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Semester)
                .HasMaxLength(30)
                .HasColumnName("semester");

            entity.HasOne(d => d.DestSpeciality).WithMany(p => p.MinGradeHistories)
                .HasForeignKey(d => d.DestSpecialityId)
                .HasConstraintName("min_grade_history_dest_speciality_id_fkey");
        });

        modelBuilder.Entity<PwrFaculty>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("pwr_faculty");
        });

        modelBuilder.Entity<PwrSpeciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pwr_speciality_pkey");

            entity.ToTable("pwr_speciality");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PwrSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pwr_subject_pkey");

            entity.ToTable("pwr_subject");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ects).HasColumnName("ects");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

            entity.HasOne(d => d.Speciality).WithMany(p => p.PwrSubjects)
                .HasForeignKey(d => d.SpecialityId)
                .HasConstraintName("pwr_subject_speciality_id_fkey");
        });

        modelBuilder.Entity<StudyArea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("study_area_pkey");

            entity.ToTable("study_area");

            entity.Property(e => e.Id)
                .HasMaxLength(4)
                .HasColumnName("id");
            entity.Property(e => e.AreaName)
                .HasMaxLength(200)
                .HasColumnName("area_name");
            entity.Property(e => e.StudyDomainId).HasColumnName("study_domain_id");

            entity.HasOne(d => d.StudyDomain).WithMany(p => p.StudyAreas)
                .HasForeignKey(d => d.StudyDomainId)
                .HasConstraintName("study_area_study_domain_id_fkey");
        });

        modelBuilder.Entity<StudyDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("study_domain_pkey");

            entity.ToTable("study_domain");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DomainName)
                .HasMaxLength(200)
                .HasColumnName("domain_name");
        });

        modelBuilder.Entity<SubjectLanguage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subject_language_pkey");

            entity.ToTable("subject_language");

            entity.HasIndex(e => e.Name, "subject_language_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.ErasmusCode).HasName("university_pkey");

            entity.ToTable("university");

            entity.Property(e => e.ErasmusCode)
                .HasMaxLength(30)
                .HasColumnName("erasmus_code");
            entity.Property(e => e.City)
                .HasMaxLength(180)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(64)
                .HasColumnName("country");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Link)
                .HasMaxLength(400)
                .HasColumnName("link");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
