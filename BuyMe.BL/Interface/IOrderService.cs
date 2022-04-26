using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL.Interface
{
    public interface IOrderService
    {
        public List<Order> GetOrders(string id);
    }
}
