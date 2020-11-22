using RmlOnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class ProductWithStocksViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public IEnumerable<StockViewModel> StocksViewModel { get; set; }
    }
}
