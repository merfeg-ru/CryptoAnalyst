using HistoryExchange.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Context
{
    public class BinanceContextConfiguration : IEntityTypeConfiguration<HistoryBinanceDTO>
    {
        public void Configure(EntityTypeBuilder<HistoryBinanceDTO> entity)
        {
            entity.HasKey(e => new { e.Id })
                .HasName("binance_pkey");
        }
    }
}
