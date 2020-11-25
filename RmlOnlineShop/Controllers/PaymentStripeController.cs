using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RmlOnlineShop.Application.LogicServices.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace RmlOnlineShop.Controllers
{
    
    public class PaymentStripeController : Controller
    {
        private readonly IConfiguration config;
        private readonly IClientLogic clientLogic;


        public PaymentStripeController(
            IConfiguration config,
            IClientLogic clientLogic

            )
        {
            this.config = config;
            this.clientLogic = clientLogic;


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
        public IActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData=new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = 2000m,
                            Currency="usd",
                            ProductData=new SessionLineItemPriceDataProductDataOptions
                            {
                                Name="T-shirt",
                            },

                        },
                        Quantity=1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return Json(new { id = session.Id });
        }

        
    }
}
