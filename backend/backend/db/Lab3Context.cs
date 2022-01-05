using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace backend.db
{
    public partial class Lab3Context : DbContext
    {
        public Lab3Context()
        {
        }

        public Lab3Context(DbContextOptions<Lab3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Dropdown> Dropdowns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Dropdown>(entity =>
            {
                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
