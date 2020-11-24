using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.SessionModels
{
    public class ProductCart
    {
       [Required]
       public int StockId { get; set; }
       [Required, Range(1,500)]
       public int Quantity { get; set; }
    }




   
}
