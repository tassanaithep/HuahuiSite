using System;
using HuahuiSite.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HuahuiSite.Infrastructure
{
    public partial class HuahuiDbContext : DbContext
    {
        public HuahuiDbContext()
        {
        }

        public HuahuiDbContext(DbContextOptions<HuahuiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MOD-NATTASIT;Database=Huahui;User Id=sa;Password=mod_nattasit;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DescriptionEn).HasColumnName("DescriptionEN");

                entity.Property(e => e.DescriptionTh)
                    .IsRequired()
                    .HasColumnName("DescriptionTH");

                entity.Property(e => e.TitleNameEn)
                    .HasColumnName("TitleNameEN")
                    .HasMaxLength(50);

                entity.Property(e => e.TitleNameTh)
                    .IsRequired()
                    .HasColumnName("TitleNameTH")
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
