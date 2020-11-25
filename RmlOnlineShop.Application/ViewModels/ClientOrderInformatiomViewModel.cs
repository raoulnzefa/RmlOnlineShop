using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels
{
    public class ClientOrderInformatiomViewModel
    {

        // Some order info (It can be vary from what user has like first name or last name so I decided to create a new properties)
        [Required]
        [Display(Name ="First Name")]
        public string FirstNameCustomer { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastNameCustomer { get; set; }
        [Required(ErrorMessage ="Email is required"), DataType(DataType.EmailAddress)]
        [Display(Name ="Email")]
        public string EmailCustomer { get; set; }
        
        public string ClientId { get; set; }

        // Address info
        [Required]
        [Display(Name = "Address Primary")]
        public string AddressPrimary { get; set; }
        [Required]
        [Display(Name = "Address Additional")]
        public string AddressSecondary { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        
        [Display(Name = "Order Comment")]
        public string OrderBuyerComment { get; set; }
    }
}
