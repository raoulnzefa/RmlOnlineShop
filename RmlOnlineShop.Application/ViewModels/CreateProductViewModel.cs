using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
