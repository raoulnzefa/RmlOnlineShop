using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RmlOnlineShop.ViewComponents
{
    public class AdminDashboardSideBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("AdminDashboardSideBar");
        }
    }
}
