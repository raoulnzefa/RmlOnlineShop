using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RmlOnlineShop.Application.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Stay Logged In?")]
        public bool IsStaySignIn { get; set; }
        public string ReturnUrl { get; set; }
    }
}
