using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RmlOnlineShop.ViewComponents
{
    public class AdminDashboardFooter : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("AdminDashboardFooter");
        }
    }
}
