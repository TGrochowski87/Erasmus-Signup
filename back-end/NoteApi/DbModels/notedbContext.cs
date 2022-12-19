using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NoteApi.DbModels
{
    public partial class notedbContext : DbContext
    {
        public notedbContext()
        {
        }

        public notedbContext(DbContextOptions<notedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommonNote> CommonNotes { get; set; } = null!;
        public virtual DbSet<Note> Notes { get; set; } = null!;
        public virtual DbSet<PlanNote> PlanNotes { get; set; } = null!;
        public virtual DbSet<SpecialityHighlightNote> SpecialityHighlightNotes { get; set; } = null!;
        public virtual DbSet<SpecialityNote> SpecialityNotes { get; set; } = null!;
        public virtual DbSet<SpecialityPriorityNote> SpecialityPriorityNotes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=notedb;Username=test;Password=123;Port=5401");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("common_note_pkey");

                entity.ToTable("common_note");

                entity.Property(e => e.NoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("note_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.CommonNote)
                    .HasForeignKey<CommonNote>(d => d.NoteId)
                    .HasConstraintName("common_note_note_id_fkey");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("note");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<PlanNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("plan_note_pkey");

                entity.ToTable("plan_note");

                entity.Property(e => e.NoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("note_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.PlanId).HasColumnName("plan_id");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.PlanNote)
                    .HasForeignKey<PlanNote>(d => d.NoteId)
                    .HasConstraintName("plan_note_note_id_fkey");
            });

            modelBuilder.Entity<SpecialityHighlightNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("speciality_highlight_note_pkey");

                entity.ToTable("speciality_highlight_note");

                entity.Property(e => e.NoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("note_id");

                entity.Property(e => e.Positive).HasColumnName("positive");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.SpecialityHighlightNote)
                    .HasForeignKey<SpecialityHighlightNote>(d => d.NoteId)
                    .HasConstraintName("speciality_highlight_note_note_id_fkey");
            });

            modelBuilder.Entity<SpecialityNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("speciality_note_pkey");

                entity.ToTable("speciality_note");

                entity.Property(e => e.NoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("note_id");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.SpecialityNote)
                    .HasForeignKey<SpecialityNote>(d => d.NoteId)
                    .HasConstraintName("speciality_note_note_id_fkey");
            });

            modelBuilder.Entity<SpecialityPriorityNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("speciality_priority_note_pkey");

                entity.ToTable("speciality_priority_note");

                entity.Property(e => e.NoteId)
                    .ValueGeneratedNever()
                    .HasColumnName("note_id");

                entity.Property(e => e.Priority).HasColumnName("priority");

                entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

                entity.HasOne(d => d.Note)
                    .WithOne(p => p.SpecialityPriorityNote)
                    .HasForeignKey<SpecialityPriorityNote>(d => d.NoteId)
                    .HasConstraintName("speciality_priority_note_note_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
