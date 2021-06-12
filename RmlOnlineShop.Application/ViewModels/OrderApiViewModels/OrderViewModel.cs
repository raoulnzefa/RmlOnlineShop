using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels.OrderApiViewModels
{
    public class OrderViewModel
    {
        public string FirstNameCustomer { get; set; }

        public string LastNameCustomer { get; set; }

        public string EmailCustomer { get; set; }

        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public IEnumerable<StockMinViewModel> Stocks { get; set; }
    }
}
