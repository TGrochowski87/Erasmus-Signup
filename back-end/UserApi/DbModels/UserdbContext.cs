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

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost:5404;Database=userdb;Username=test;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_profile_pkey");

            entity.ToTable("user_profile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AverageGrade).HasColumnName("average_grade");
            entity.Property(e => e.PreferencedStudyDomainId).HasColumnName("preferenced_study_domain_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
