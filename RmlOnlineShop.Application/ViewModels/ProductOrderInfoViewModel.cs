using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class ProductOrderInfoViewModel
    {
        public string Name { get; set; }

        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public string StockDescription { get; set; }
        public int Quantity { get; set; }

    }
}
