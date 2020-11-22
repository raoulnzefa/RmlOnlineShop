using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class CreateStockViewModel
    {
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ProductId { get; set; }
       
    }
}
