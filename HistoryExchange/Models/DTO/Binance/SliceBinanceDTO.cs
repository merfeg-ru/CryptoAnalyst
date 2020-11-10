using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryExchange.Models
{
    [Table("SliceBinance")]
    public class SliceBinanceDTO : SliceItemDTO 
    {
        public List<HistoryBinanceDTO> Items { get; set; }

        public SliceBinanceDTO()
        {
            Items = new List<HistoryBinanceDTO>();
        }
    }
}
