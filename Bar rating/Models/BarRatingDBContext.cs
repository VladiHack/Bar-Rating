using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bar_rating.Models
{
    public partial class BarRatingDBContext : DbContext
    {
        public BarRatingDBContext()
        {
        }

        public BarRatingDBContext(DbContextOptions<BarRatingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bar> Bars { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=BarRatingDB;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bar>(entity =>
            {
                entity.ToTable("Bar");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BarImage).HasColumnType("image");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Review");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ReviewText)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bar)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BarId)
                    .HasConstraintName("FK__Review__BarId__5165187F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__UserId__5070F446");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
