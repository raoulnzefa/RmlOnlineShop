using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class ProductsInCartViewModel
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string StockDescription { get; set; }
        

        public int Quantity { get; set; } // Quantity of the order
    }

    
}
