using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RmlOnlineShop.Application.DataServices;
using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Data.Models;
using RmlOnlineShop.Application.ViewModels;
using Microsoft.AspNetCore.Http;

namespace RmlOnlineShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> loggerProductsController;
        private readonly IProductManager productManager;

        public ProductsController
            (
                ILogger<ProductsController> loggerProductsController,
                IProductManager productManager
            )
        {
            this.loggerProductsController = loggerProductsController;
            this.productManager = productManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(productManager.GetAllProductsAsViewModel());
        }

        [HttpGet]
        public IActionResult Product(int id)
        {
            var product = productManager.GetProductViewModelById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult ProductPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var current_id = HttpContext.Session.GetString("id");

            HttpContext.Session.SetString("id", id.ToString());
            
            return RedirectToAction("Index", "Products");
        }

    }
}
