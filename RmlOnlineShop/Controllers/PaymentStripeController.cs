using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using RmlOnlineShop.Application.ViewModels;
using Stripe;
using Stripe.Checkout;

namespace RmlOnlineShop.Controllers
{
    
    public class PaymentStripeController : Controller
    {
        private readonly IConfiguration config;
        private readonly IClientLogic clientLogic;
        private readonly ICartLogic cartLogic;

        public PaymentStripeController(
            IConfiguration config,
            IClientLogic clientLogic,
            ICartLogic cartLogic
            )
        {
            this.config = config;
            this.clientLogic = clientLogic;
            this.cartLogic = cartLogic;

            StripeConfiguration.ApiKey = config.GetSection("Stripe")["SecretKey"];

        }


        public IActionResult Payment()
        {
            var clientOrderInfo = clientLogic.GetClientOrderInfo(HttpContext.Session);
            if (clientOrderInfo == null)
            {
                return RedirectToAction("ClientCheckout");
            }


            ViewData["PublicKey"] = config.GetSection("Stripe")["PublicKey"];



            return View();
        }

        [HttpPost("checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var products = cartLogic.GetProductInCartAsViewModel(HttpContext.Session);

            if (products==null)
            {
                RedirectToAction("Index", "Products");
            }
            
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            foreach (var product in products)
            {
                options.LineItems.Add(new SessionLineItemOptions { 
                    PriceData=new SessionLineItemPriceDataOptions
                    {
                        UnitAmountDecimal = product.ProductPrice * 100,
                        Currency="rub",
                        ProductData=new SessionLineItemPriceDataProductDataOptions
                        {
                            Name=product.ProductName,
                            Description=product.ProductDescription
                        },
                    },
                    Quantity=product.Quantity
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);
            
            var prodOrders = clientLogic.GetProductsAndOrderInfo(HttpContext.Session);

            await clientLogic.SaveOrder(new OrderInfoViewModel
            {
                AddressPrimary = prodOrders.ClientOrderInformatiomViewModel.AddressPrimary,
                AddressSecondary= prodOrders.ClientOrderInformatiomViewModel.AddressSecondary,
                City= prodOrders.ClientOrderInformatiomViewModel.City,
                Country= prodOrders.ClientOrderInformatiomViewModel.Country,
                EmailCustomer= prodOrders.ClientOrderInformatiomViewModel.EmailCustomer,
                FirstNameCustomer= prodOrders.ClientOrderInformatiomViewModel.FirstNameCustomer,
                LastNameCustomer= prodOrders.ClientOrderInformatiomViewModel.LastNameCustomer,
                OrderBuyerComment= prodOrders.ClientOrderInformatiomViewModel.OrderBuyerComment,
                PostCode= prodOrders.ClientOrderInformatiomViewModel.PostCode,
                Stocks= prodOrders.ProductsInCartViewModel.Select(x=> new StockMinViewModel
                {
                    Quantity=x.Quantity,
                    StockId=x.StockId
                }),
                StripeOrderRef=session.ClientReferenceId,
                SessionId=HttpContext.Session.Id
            });



            return Json(new { id = session.Id });
        }

        
    }
}
