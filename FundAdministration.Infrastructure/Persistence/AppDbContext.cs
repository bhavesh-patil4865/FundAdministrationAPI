using FundAdministration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundAdministration.Infrastructure.Persistence
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Fund> Funds => Set<Fund>();

        public DbSet<Investor> Investors => Set<Investor>();

        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fund>(entity =>
            {
                entity.HasKey(x => x.FundId);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Currency)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Investor>(entity =>
            {
                entity.HasKey(x => x.InvestorId);

                entity.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(x => x.Fund)
                    .WithMany(f => f.Investors)
                    .HasForeignKey(x => x.FundId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(x => x.TransactionId);

                entity.Property(x => x.Amount)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(x => x.Investor)
                    .WithMany(i => i.Transactions)
                    .HasForeignKey(x => x.InvestorId);
            });
        }

    }
}
