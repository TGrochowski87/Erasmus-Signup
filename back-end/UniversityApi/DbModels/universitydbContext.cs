using Microsoft.EntityFrameworkCore;

namespace UniversityApi.DbModels
{
    public partial class universitydbContext : DbContext
    {
        public universitydbContext()
        {
        }

        public universitydbContext(DbContextOptions<universitydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ContractDetail> ContractDetails { get; set; } = null!;
        public virtual DbSet<ContractSpecificDetail> ContractSpecificDetails { get; set; } = null!;
        public virtual DbSet<DestSpeciality> DestSpecialities { get; set; } = null!;
        public virtual DbSet<ExcelFormat> ExcelFormats { get; set; } = null!;
        public virtual DbSet<MinGradeHistory> MinGradeHistories { get; set; } = null!;
        public virtual DbSet<PwrFaculty> PwrFaculties { get; set; } = null!;
        public virtual DbSet<PwrSpeciality> PwrSpecialities { get; set; } = null!;
        public virtual DbSet<PwrSubject> PwrSubjects { get; set; } = null!;
        public virtual DbSet<StudyArea> StudyAreas { get; set; } = null!;
        public virtual DbSet<SubjectLanguage> SubjectLanguages { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("Host=localhost:5400;Database=universitydb;Username=test;Password=123");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum("t_pwr_fac", new[] { "Ogólnouczelniana", "Architektury", "Budownictwa", "Chemiczny", "Informatyki i Telekomunikacji", "Elektryczny", "Geoinżynierii", " Górnictwa i Geologii", "Inżynierii Środowiska", "Zarządzania", "Mechaniczno-Energetyczny", "Mechaniczny", "Podstawowych Problemów Techniki", "Elektroniki Fotoniki i Mikrosystemów", "Matematyki", "Wydział Zamiejscowy PWr", "Techniczno-Inżynieryjny ZOD" })
                .HasPostgresEnum("t_pwr_fac_sh", new[] { "PWr", "W1", "W2", "W3", "W4N", "W5", "W6", "W7", "W8N", "W9", "W10", "W11", "W12N", "W13", "W15", "F3" })
                .HasPostgresExtension("citext");

            modelBuilder.Entity<ContractDetail>(entity =>
            {
                entity.ToTable("contract_details");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcceptingDoctoral).HasColumnName("accepting_doctoral");

                entity.Property(e => e.AcceptingPostgraduate).HasColumnName("accepting_postgraduate");

                entity.Property(e => e.AcceptingUndergraduate).HasColumnName("accepting_undergraduate");

                entity.Property(e => e.ConclusionDate).HasColumnName("conclusion_date");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.SpecificDetailsId).HasColumnName("specific_details_id");

                entity.HasOne(d => d.SpecificDetails)
                    .WithMany(p => p.ContractDetails)
                    .HasForeignKey(d => d.SpecificDetailsId)
                    .HasConstraintName("contract_details_specific_details_id_fkey");
            });

            modelBuilder.Entity<ContractSpecificDetail>(entity =>
            {
                entity.ToTable("contract_specific_details");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DoctoralYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("doctoral_year_restriction");

                entity.Property(e => e.IsAggregate).HasColumnName("is_aggregate");

                entity.Property(e => e.PostgraduateYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("postgraduate_year_restriction");

                entity.Property(e => e.UndergraduateYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("undergraduate_year_restriction");
            });

            modelBuilder.Entity<DestSpeciality>(entity =>
            {
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

                entity.HasOne(d => d.ContractDetails)
                    .WithMany(p => p.DestSpecialities)
                    .HasForeignKey(d => d.ContractDetailsId)
                    .HasConstraintName("dest_speciality_contract_details_id_fkey");

                entity.HasOne(d => d.DestUniversityCodeNavigation)
                    .WithMany(p => p.DestSpecialities)
                    .HasForeignKey(d => d.DestUniversityCode)
                    .HasConstraintName("dest_speciality_dest_university_code_fkey");

                entity.HasOne(d => d.StudyArea)
                    .WithMany(p => p.DestSpecialities)
                    .HasForeignKey(d => d.StudyAreaId)
                    .HasConstraintName("dest_speciality_study_area_id_fkey");

                entity.HasOne(d => d.SubjectLanguage)
                    .WithMany(p => p.DestSpecialities)
                    .HasForeignKey(d => d.SubjectLanguageId)
                    .HasConstraintName("dest_speciality_subject_language_id_fkey");
            });

            modelBuilder.Entity<ExcelFormat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("excel_format");

                entity.Property(e => e.AcceptingDoctoral).HasColumnName("accepting_doctoral");

                entity.Property(e => e.AcceptingPostgraduate).HasColumnName("accepting_postgraduate");

                entity.Property(e => e.AcceptingUndergraduate).HasColumnName("accepting_undergraduate");

                entity.Property(e => e.AreaDescription)
                    .HasMaxLength(200)
                    .HasColumnName("area_description");

                entity.Property(e => e.ConclusionDate).HasColumnName("conclusion_date");

                entity.Property(e => e.Country)
                    .HasMaxLength(60)
                    .HasColumnName("country");

                entity.Property(e => e.DoctoralYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("doctoral_year_restriction");

                entity.Property(e => e.ErasmusCode)
                    .HasMaxLength(30)
                    .HasColumnName("erasmus_code");

                entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");

                entity.Property(e => e.PostgraduateYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("postgraduate_year_restriction");

                entity.Property(e => e.StudyDomain)
                    .HasMaxLength(4)
                    .HasColumnName("study_domain");

                entity.Property(e => e.SubLanguage)
                    .HasMaxLength(200)
                    .HasColumnName("sub_language");

                entity.Property(e => e.UndergraduateYearRestriction)
                    .HasMaxLength(10)
                    .HasColumnName("undergraduate_year_restriction");

                entity.Property(e => e.UniversityName)
                    .HasMaxLength(200)
                    .HasColumnName("university_name");
            });

            modelBuilder.Entity<MinGradeHistory>(entity =>
            {
                entity.ToTable("min_grade_history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DestSpecialityId).HasColumnName("dest_speciality_id");

                entity.Property(e => e.Grade).HasColumnName("grade");

                entity.Property(e => e.Semester)
                    .HasMaxLength(30)
                    .HasColumnName("semester");

                entity.HasOne(d => d.DestSpeciality)
                    .WithMany(p => p.MinGradeHistories)
                    .HasForeignKey(d => d.DestSpecialityId)
                    .HasConstraintName("min_grade_history_dest_speciality_id_fkey");
            });

            modelBuilder.Entity<PwrFaculty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pwr_faculty");
            });

            modelBuilder.Entity<PwrSpeciality>(entity =>
            {
                entity.ToTable("pwr_speciality");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PwrSubject>(entity =>
            {
                entity.ToTable("pwr_subject");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ects).HasColumnName("ects");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.HasOne(d => d.Speciality)
                    .WithMany(p => p.PwrSubjects)
                    .HasForeignKey(d => d.SpecialityId)
                    .HasConstraintName("pwr_subject_speciality_id_fkey");
            });

            modelBuilder.Entity<StudyArea>(entity =>
            {
                entity.HasKey(e => e.StudyDomain)
                    .HasName("study_area_pkey");

                entity.ToTable("study_area");

                entity.Property(e => e.StudyDomain)
                    .HasMaxLength(4)
                    .HasColumnName("study_domain");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<SubjectLanguage>(entity =>
            {
                entity.ToTable("subject_language");

                entity.HasIndex(e => e.Name, "subject_language_name_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.HasKey(e => e.ErasmusCode)
                    .HasName("university_pkey");

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
                    .HasColumnType("citext")
                    .HasColumnName("email");

                entity.Property(e => e.Link)
                    .HasColumnType("citext")
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
