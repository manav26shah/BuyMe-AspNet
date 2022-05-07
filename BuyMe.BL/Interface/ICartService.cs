using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Interface
{
    public interface ICartService
    {
        Task<bool> AddToCart(CartBL newCart);
        Task<bool> UpdateToCart(CartBL newCart);
        Task<bool> DeleteFromCart(int productId);
        Task<bool> Checkout(string userId);
    }
}
