using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;
using RmlOnlineShop.Data.Models;
using RmlOnlineShop.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.LogicServices
{
    public class ClientLogic : IClientLogic
    {
        private readonly ICartLogic cartLogic;
        private readonly ApplicationDbContext applicationDbContext;

        public ClientLogic(
            ICartLogic cartLogic,
            ApplicationDbContext applicationDbContext
            )
        {
            this.cartLogic = cartLogic;
            this.applicationDbContext = applicationDbContext;
        }


        public ClientOrderInformatiomViewModel GetClientOrderInfo(ISession session)
        {
            var s = session.GetString("ClientOrderInfo");
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ClientOrderInformatiomViewModel>(s);

        }

        public bool SaveClientOrderInfo(ISession session,ClientOrderInformatiomViewModel clientOrderInformatiomViewModel)
        {
            if (clientOrderInformatiomViewModel == null)
            {
                return false;
            }

            var clientInfo = JsonConvert.SerializeObject(clientOrderInformatiomViewModel);

            session.SetString("ClientOrderInfo", clientInfo);
            return true;
        }

        public ProductsAndOrderInfoViewModel GetProductsAndOrderInfo(ISession session)
        {
            var productsInCart = cartLogic.GetProductInCartAsViewModel(session);
            if (productsInCart==null)
            {
                return null;
            }

            var clientOrderInfo = GetClientOrderInfo(session);
           

            var ProductsAndClientOrderInfo = new ProductsAndOrderInfoViewModel
            {
                ClientOrderInformatiomViewModel=clientOrderInfo,
                ProductsInCartViewModel=productsInCart
            };

            return ProductsAndClientOrderInfo;
        }
         
        public OrderInfoFullViewModel GetOrderByUniqueId(string uniqueId)
        {
            var order = applicationDbContext.Orders
                .Where(x => x.OrderUniqueId == uniqueId)
                    .Include(x => x.OrderStocks)
                        .ThenInclude(x => x.Stock)
                            .ThenInclude(x => x.Product)
                .Select(x => new OrderInfoFullViewModel
                {
                    OrderUniqueId= x.OrderUniqueId,
                    AddressPrimary=x.AddressPrimary,
                    AddressSecondary=x.AddressSecondary,
                    City=x.City,
                    ClientId=x.ClientId,
                    Country=x.Country,
                    EmailCustomer=x.EmailCustomer,
                    FirstNameCustomer=x.FirstNameCustomer,
                    LastNameCustomer=x.LastNameCustomer,
                    OrderBuyerComment=x.OrderBuyerComment,
                    PostCode=x.PostCode,
                    ProductsOrderInfo=x.OrderStocks.Select(s=>new ProductOrderInfoViewModel
                    {
                        Name=s.Stock.Product.Name,
                        Price=s.Stock.Product.Price,
                        ProductDescription=s.Stock.Product.Description,
                        Quantity=s.Quantity,
                        StockDescription=s.Stock.Description
                    })
                 
                })
                .FirstOrDefault();

            return order;

        }


        public async Task<bool> SaveOrder(OrderInfoViewModel orderInfoViewModel)
        {
            if (orderInfoViewModel==null)
            {
                return false;
            }

            // Delete all reserved stocks since we made the order
            var reservedStocks = applicationDbContext.stocksReservedOnOrder
                .Where(x => x.SessionId == orderInfoViewModel.SessionId)
                .AsEnumerable();

            applicationDbContext.stocksReservedOnOrder.RemoveRange(reservedStocks);

            Order order = new Order
            {
                AddressPrimary = orderInfoViewModel.AddressPrimary,
                AddressSecondary = orderInfoViewModel.AddressSecondary,
                City = orderInfoViewModel.City,
                ClientId = orderInfoViewModel.ClientId,
                Country = orderInfoViewModel.Country,
                EmailCustomer = orderInfoViewModel.EmailCustomer,
                FirstNameCustomer = orderInfoViewModel.FirstNameCustomer,
                LastNameCustomer = orderInfoViewModel.LastNameCustomer,
                OrderBuyerComment = orderInfoViewModel.OrderBuyerComment,
                PostCode = orderInfoViewModel.PostCode,
                StripeRef = orderInfoViewModel.StripeOrderRef,
                OrderStocks = orderInfoViewModel.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Quantity = x.Quantity
                }).ToList(),
                OrderUniqueId = GenerateOrderUniqueId(DateTime.Now.ToString("dd:mm"))
            };

            applicationDbContext.Orders.Add(order);
            var res = await applicationDbContext.SaveChangesAsync();
            return true;
        }

        private string GenerateOrderUniqueId(string id)
        {
            Guid guid = Guid.NewGuid();
            var s = Convert.ToBase64String(guid.ToByteArray()).Replace("+", "s");
            return s + id;
        }

    }

}
