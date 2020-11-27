using Microsoft.AspNetCore.Http;
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
         
        public async Task<bool> SaveOrder(OrderInfoViewModel orderInfoViewModel)
        {
            if (orderInfoViewModel==null)
            {
                return false;
            }


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
                OrderUniqueId = GenerateOrderUniqueId(orderInfoViewModel.ClientId),
            };

            applicationDbContext.Orders.Add(order);
            var res = await applicationDbContext.SaveChangesAsync();
            return true;
        }

        private string GenerateOrderUniqueId(string id)
        {
            Guid guid = Guid.NewGuid();
            return Convert.ToBase64String(guid.ToByteArray()) + id;
        }

    }

}
