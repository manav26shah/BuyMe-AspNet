using BuyMe.BL.Interface;
using BuyMe.DL;
using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Implementation
{
    public class CartService : ICartService
    {
        private IRepo _repo;

        public CartService(IRepo repo)
        {
            _repo = repo;
        }
        
        public async Task<bool> AddToCart(CartBL newCart)
        {
            var entity = new Cart
            {
                ProductId = newCart.ProductId,
                Email = newCart.Email,
            };
            return await _repo.AddToCart(entity);
        }

        public async Task<bool> UpdateToCart(CartBL newCart)
        {
            var entity = new Cart
            {
                ProductId = newCart.ProductId,
                Email = newCart.Email,
            };
            return await _repo.UpdateToCart(entity);
        }

        public async Task<bool> DeleteFromCart(int productId)
        {
            return await _repo.DeleteFromCart(productId);
        }
        public async Task<bool> Checkout(string userId)
        {
            return await _repo.Checkout(userId);
        }
    }
}
