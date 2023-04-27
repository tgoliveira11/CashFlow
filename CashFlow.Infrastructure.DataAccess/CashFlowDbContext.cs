using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Infrastructure.DataAccess
{
    public class CashFlowDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }

        public CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entry>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Entry>()
                .Property(e => e.Date)
                .IsRequired();

            modelBuilder.Entity<Entry>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            modelBuilder.Entity<Entry>()
                .Property(e => e.Type)
                .IsRequired();

            modelBuilder.Entity<Entry>()
                .Property(e => e.Description);
        }
    }
}
