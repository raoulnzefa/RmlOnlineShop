using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RmlOnlineShop.ViewComponents
{
    public class StoreFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("StoreFooter");
        }
    }
}
