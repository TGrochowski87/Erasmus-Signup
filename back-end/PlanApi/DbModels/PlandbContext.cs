using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlanApi.DbModels;

public partial class PlandbContext : DbContext
{
    public PlandbContext()
    {
    }

    public PlandbContext(DbContextOptions<PlandbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HomeSubject> HomeSubjects { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=erasmus-db.postgres.database.azure.com;Database=plan;Username=postgres;Password=Erasmus123;Port=5432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HomeSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("home_subject_pkey");

            entity.ToTable("home_subject");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ects).HasColumnName("ects");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("plan_pkey");

            entity.ToTable("plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subject_pkey");

            entity.ToTable("subject");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ects).HasColumnName("ects");
            entity.Property(e => e.MappedSubject).HasColumnName("mapped_subject");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");

            entity.HasOne(d => d.MappedSubjectNavigation).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.MappedSubject)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("subject_mapped_subject_fkey");

            entity.HasOne(d => d.Plan).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("subject_plan_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
