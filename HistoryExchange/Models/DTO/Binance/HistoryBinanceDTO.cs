using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Models
{
    [Table("HistoryBinance")]
    public class HistoryBinanceDTO : HistoryItemDTO 
    {
        public SliceBinanceDTO Slice { get; set; }
    }
}
