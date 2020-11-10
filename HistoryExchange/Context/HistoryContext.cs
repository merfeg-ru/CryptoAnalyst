using HistoryExchange.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Context
{
    public class HistoryContext : DbContext
    {
        public DbSet<HistoryBinanceDTO> HistoryBinance { get; set; }

        public HistoryContext(DbContextOptions<HistoryContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BinanceContextConfiguration());
        }
    }
}
