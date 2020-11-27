using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class ProductsAndOrderInfoViewModel
    {
        public ClientOrderInformatiomViewModel ClientOrderInformatiomViewModel { get; set; }
        public IEnumerable<ProductsInCartViewModel> ProductsInCartViewModel { get; set; }
    }
}
