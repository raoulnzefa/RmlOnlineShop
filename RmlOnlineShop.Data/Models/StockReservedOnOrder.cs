using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Data.Models
{
    public class StockReservedOnOrder
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int QuantitySaved { get; set; }
        public DateTime HoldUntillDate { get; set; }
    }
}
