using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RmlOnlineShop.Application.DataServices;
using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;

namespace RmlOnlineShop.Controllers
{
    
    [Authorize(Policy ="Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly IProductManager productManager;
        private readonly IAdminDashboardLogic adminDashboardLogic;
        private readonly IStockManager stockManager;

        public AdminDashboardController(
            IProductManager productManager,
            IAdminDashboardLogic adminDashboardLogic,
            IStockManager stockManager
            )
        {
            this.productManager = productManager;
            this.adminDashboardLogic = adminDashboardLogic;
            this.stockManager = stockManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Stock()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }


        #region REST_ACTIONS
        #region STOCK_ACTIONS

        [HttpGet]
        public IActionResult GetAllStocks()
        {
            return Ok(stockManager.GetAllProductsWithStocks());
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteStock(int id)
        {
            if (id<0)
            {
                return BadRequest("Id Can't be negative number");
            }

            return Ok(await stockManager.DeleteStock(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStock([FromBody] StockViewModel stockViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Stock Parameters");
            }
            return Ok(await stockManager.UpdateStockByViewModel(stockViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] CreateStockViewModel createStockViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad Stock Parameters");
            }
            var res = await stockManager.CreateStockByViewModel(createStockViewModel);
            if (res == null)
            {
                return BadRequest("Bad Stock Parameters");
            }
            return Ok(res);
        }
        #endregion
        #region PRODUCT_ACTIONS

       
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            return Ok(await productManager.DeleteProduct(id));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await productManager.UpdateProductByViewModel(productViewModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await productManager.CreateProductByViewModel(productViewModel));
        }
        #endregion
        #endregion
    }
}
