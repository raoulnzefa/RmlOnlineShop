using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
   public class OrderInfoViewModel
    {
        public string StripeOrderRef { get; set; }
        public string FirstNameCustomer { get; set; }
   
        public string LastNameCustomer { get; set; }
     
        public string EmailCustomer { get; set; }

        public string ClientId { get; set; }
        public string SessionId { get; set; }

     
        public string AddressPrimary { get; set; }
   
        public string AddressSecondary { get; set; }
     
        public string City { get; set; }
     
        public string Country { get; set; }
       
        public string PostCode { get; set; }

        public string OrderBuyerComment { get; set; }

        public IEnumerable<StockMinViewModel> Stocks { get; set; }

    }
}
