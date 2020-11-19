using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RmlOnlineShop.Application.DataServices;
using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;

namespace RmlOnlineShop.Controllers
{
    
    public class AdminDashboardController : Controller
    {
        private readonly IProductManager productManager;
        public AdminDashboardController(
            IProductManager productManager
            )
        {
            this.productManager = productManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(productManager.GetAllProductsAsViewModel());
        }

        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            if (id<0)
            {
                return BadRequest();
            }
            return Ok(productManager.GetProductById(id));
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            return Ok(productManager.DeleteProduct(id));
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(productManager.UpdateProductByViewModel(productViewModel));
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(productManager.CreateProductByViewModel(productViewModel));
        }

    }
}
