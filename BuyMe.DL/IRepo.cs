using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.DL
{
    public interface IRepo
    {
        List<Product> GetProducts(int page, double pageResult);
        Task<bool> AddNewProduct(Product newProduct);
        List<Product> GetProductByMatch(string exp);
        List<Order> GetOrders(string userId);
        List<Product> GetProductsByCId(int id, int page, double pageResult );
        Task<bool> AddToCart(Cart cart);

        Task<bool> UpdateToCart(Cart cart);
        Task<bool> DeleteFromCart(int productId);

    }
}
