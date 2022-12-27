using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserApi.DbModels;

public partial class UserdbContext : DbContext
{
    public UserdbContext()
    {
    }

    public UserdbContext(DbContextOptions<UserdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Coordinator> Coordinators { get; set; }

    public virtual DbSet<DestinationList> DestinationLists { get; set; }

    public virtual DbSet<DestinationListPreference> DestinationListPreferences { get; set; }

    public virtual DbSet<DestinationListType> DestinationListTypes { get; set; }

    public virtual DbSet<Nofitifaction> Nofitifactions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Student> Students { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost:5404;Database=userdb;Username=test;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("t_pwr_fac_sh", new[] { "PWR", "W1", "W2", "W3", "W4N", "W5", "W6", "W7", "W8N", "W9", "W10", "W11", "W12N", "W13", "W15", "F3" })
            .HasPostgresExtension("citext");

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("app_user_pkey");

            entity.ToTable("app_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("citext")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("app_user_role_id_fkey");
        });

        modelBuilder.Entity<Coordinator>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("coordinator_pkey");

            entity.ToTable("coordinator");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.PwrSpeciality).HasColumnName("pwr_speciality");

            entity.HasOne(d => d.User).WithOne(p => p.Coordinator)
                .HasForeignKey<Coordinator>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("coordinator_user_id_fkey");
        });

        modelBuilder.Entity<DestinationList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("destination_list_pkey");

            entity.ToTable("destination_list");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DestinationId).HasColumnName("destination_id");
            entity.Property(e => e.ListTypeId).HasColumnName("list_type_id");
            entity.Property(e => e.PreferenceListId).HasColumnName("preference_list_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ListType).WithMany(p => p.DestinationLists)
                .HasForeignKey(d => d.ListTypeId)
                .HasConstraintName("destination_list_list_type_id_fkey");

            entity.HasOne(d => d.PreferenceList).WithMany(p => p.DestinationLists)
                .HasForeignKey(d => d.PreferenceListId)
                .HasConstraintName("destination_list_preference_list_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.DestinationLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("destination_list_user_id_fkey");
        });

        modelBuilder.Entity<DestinationListPreference>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("destination_list_preference_pkey");

            entity.ToTable("destination_list_preference");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DestinationListType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("destination_list_type_pkey");

            entity.ToTable("destination_list_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Nofitifaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nofitifaction_pkey");

            entity.ToTable("nofitifaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("url");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Nofitifactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("nofitifaction_user_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.AverageGrade).HasColumnName("average_grade");
            entity.Property(e => e.Index)
                .HasMaxLength(50)
                .HasColumnName("index");
            entity.Property(e => e.PwrSpeciality).HasColumnName("pwr_speciality");
            entity.Property(e => e.Semester).HasColumnName("semester");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
