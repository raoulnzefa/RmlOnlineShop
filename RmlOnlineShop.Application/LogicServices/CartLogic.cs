using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using RmlOnlineShop.Application.SessionModels;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;
using RmlOnlineShop.Data.Models;
using RmlOnlineShop.Database.DatabaseContext;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RmlOnlineShop.Application.LogicServices
{
   public class CartLogic : ICartLogic
    {

        private readonly ApplicationDbContext applicationDbContext;
        private readonly IConfiguration configuration;

        public CartLogic(
            ApplicationDbContext applicationDbContext,
            IConfiguration configuration
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.configuration = configuration;
        }


        public List<ProductCart> GetCart(ISession session)
        {
            var cartString = session.GetString("cart");
            if (cartString == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<ProductCart>>(cartString);
        }

        public async Task<bool> AddToCart(ISession session,ProductCart productCart)
        {
            if (productCart == null)
            {
                return false;
            }

            var reservedStock = applicationDbContext.Stocks
                .FirstOrDefault(x => x.Id == productCart.StockId);

            var reservedStocksToUpdate = applicationDbContext.stocksReservedOnOrder
                .Where(x => x.SessionId == session.Id)
                .AsEnumerable();

            foreach (var stock in reservedStocksToUpdate)
            {
                stock.HoldUntillDate = DateTime.Now.AddMinutes(60.0);
            }
            
            if (reservedStock==null)
            {
                return false;
            }

            if (reservedStock.Quantity < productCart.Quantity)
            {
                return false;
            }
            
            applicationDbContext.stocksReservedOnOrder.Add(new StockReservedOnOrder
            {
                QuantitySaved = productCart.Quantity,
                StockId = productCart.StockId,
                HoldUntillDate = DateTime.Now.AddMinutes(60.0),
                SessionId=session.Id
            });

            reservedStock.Quantity -= productCart.Quantity;


            await applicationDbContext.SaveChangesAsync();


            var cartString = session.GetString("cart");
            var cartList = new List<ProductCart>();

            if (cartString != null)
            {
                cartList = JsonConvert.DeserializeObject<List<ProductCart>>(cartString);
            }


            if (cartList.Any(x => x.StockId == productCart.StockId))
            {
                cartList.Find(x => x.StockId == productCart.StockId).Quantity += productCart.Quantity;
            }
            else
            {
                cartList.Add(productCart);
            }



            cartString = JsonConvert.SerializeObject(cartList);
            session.SetString("cart", cartString);
            return true;
        }

        public IEnumerable<ProductsInCartViewModel> GetProductInCartAsViewModel(ISession session)
        {

            var productsCartList = GetCart(session);
            if (productsCartList == null)
            {
                return null;
            }

            // TODO: FIX THIS QUERY WITH EXPLICIT CLIENT EVALUATION WHICH CAN LEAD TO PERFOMANCE DROP!!!
            var products = applicationDbContext.Stocks
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => productsCartList.Any(stock => stock.StockId == x.Id))
                .Select(x => new ProductsInCartViewModel
                {
                    StockId=x.Id,
                    ProductId = x.ProductId,
                    ProductDescription = x.Product.Description,
                    ProductName = x.Product.Name,
                    ProductPrice = x.Product.Price,
                    Quantity = productsCartList.FirstOrDefault(stock => stock.StockId == x.Id).Quantity,
                    StockDescription = x.Description
                });

            return products;
        }
    }
}
