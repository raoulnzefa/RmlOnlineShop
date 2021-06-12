using RmlOnlineShop.Application.DataServices.Interfaces;
using RmlOnlineShop.Application.ViewModels.OrderApiViewModels;
using RmlOnlineShop.Database.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RmlOnlineShop.Application.DataServices
{
    public class OrderManager : IOrderManager
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderManager(
            ApplicationDbContext applicationDbContext
            )
        {
            this.applicationDbContext = applicationDbContext;
        }


        //public IEnumerable<OrderViewModel> GetAllOrders()
        //{
        //    return applicationDbContext.Orders
        //        .Select()
        //}


    }
}
