using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL.Implementation
{
    public class OrderService : IOrderService
    {
        private IRepo _repo;

        public OrderService(IRepo repo)
        {
            _repo = repo;
        }
        public List<Order> GetOrders(string Id)
        {
            return _repo.GetOrders(Id);
        }
    }
}
