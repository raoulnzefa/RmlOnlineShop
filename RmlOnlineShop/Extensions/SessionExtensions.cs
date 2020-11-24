using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RmlOnlineShop.Application.SessionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RmlOnlineShop.Extensions
{
    public static class SessionExtensions
    {

        public static bool SetCart(this ISession session, ProductCart productCart)
        {
            if (productCart == null)
            {
                return false;
            }

            string ProductCartAsString = JsonConvert.SerializeObject(productCart);

            session.SetString("cart", ProductCartAsString);
            return true;
        }

        public static ProductCart GetCart(this ISession session)
        {
            var stringObject = session.GetString("cart");
            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ProductCart>(stringObject);
        }

    }
}
