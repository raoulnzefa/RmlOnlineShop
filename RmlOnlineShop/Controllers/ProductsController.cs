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
            return View(productManager.GetAllProducts());
        }

        [HttpGet]
        public IActionResult Product(int id)
        {
            var product = productManager.GetProductViewModelById(id);

            return View(product);
        }
     

        
    }
}
