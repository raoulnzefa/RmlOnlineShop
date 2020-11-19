using System;
using System.Collections.Generic;
using System.Text;

namespace RmlOnlineShop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderUrl { get; set; }
        public string AddressPrimary { get; set; }
        public string AddressSecondary { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string OrderBuyerComment { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
