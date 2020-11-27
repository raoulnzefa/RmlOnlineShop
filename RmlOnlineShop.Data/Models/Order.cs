using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderUrl { get; set; }

        // Some order info (It can be vary from what user has like first name or last name so I decided to create a new properties)
        public string FirstNameCustomer { get; set; }
        public string LastNameCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string ClientId { get; set; }
        public string StripeRef { get; set; }

        // Address info
        public string AddressPrimary { get; set; }
        public string AddressSecondary { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        
        public string OrderBuyerComment { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
