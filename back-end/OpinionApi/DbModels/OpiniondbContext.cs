using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OpinionApi.DbModels;

public partial class OpiniondbContext : DbContext
{
    public OpiniondbContext()
    {
    }

    public OpiniondbContext(DbContextOptions<OpiniondbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Opinion> Opinions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=erasmus-db.postgres.database.azure.com;Database=opinion;Port=5432;Username=postgres;Password=Erasmus123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Opinion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("opinion_pkey");

            entity.ToTable("opinion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
