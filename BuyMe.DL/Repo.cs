using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuyMe.DL
{

    // Generic Repository pattern
    public class Repo:IRepo
    {
        private AppDbContext _dbContext;

        public Repo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<bool> AddNewProduct(Product newProduct)
        {
            _dbContext.Products.Add(newProduct);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public List<Product> GetProducts(int page, double pageResult)
        {
            var pageCount = Math.Ceiling(_dbContext.Products.Count() / pageResult);

            return _dbContext.Products.
                Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
        }

        public List<Product> GetProductsByCId(int id, int page, double pageResult)
        {
            var pageCount = Math.Ceiling(_dbContext.Products.Count() / pageResult);

            var products = _dbContext.Products.Where(c => c.CategoryId == id);

            return products.Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();
        }

        public List<Product> GetProductByMatch(string exp)
        {
            exp = "%" + exp + "%";
            var products = _dbContext.Products.Where(c => EF.Functions.Like(c.Name, exp));
            return products.ToList();
        }

        public List<Order> GetOrders(string userId)
        {
            var _order = _dbContext.Orders.Where(c => EF.Functions.Like(c.Email, userId));
            /*List<Product> result = new List<Product>();
            foreach (var order in _order)
            {
                result.Add((Product)_dbContext.Orders.Where(c => c.ProductId == order.ProductId));
            }
            return result;*/ 
           return _order.ToList();
        }

        public async Task<bool> AddToCart(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateToCart(Cart cart)
        {
            var entity = _dbContext.Carts.FirstOrDefault(c => c.Email == cart.Email);
            if(entity == null)
            {
                return false;
            }
            entity.ProductId = cart.ProductId;
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteFromCart(int productId)
        {
            var removeProduct = _dbContext.Carts.Where(c => c.ProductId == productId).First();
            _dbContext.Carts.Remove(removeProduct);
            var result = await _dbContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
    }
}
