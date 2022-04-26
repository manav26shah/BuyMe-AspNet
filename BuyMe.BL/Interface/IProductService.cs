using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Interface
{
    public interface IProductService
    {
        public List<Product> GetProducts(int page, double pageResult);
        public Task<bool> AddNewProduct(ProductBL newProduct);
        public List<Product> GetProductByMatch(string exp);
        public List<Product> GetProductsByCId(int id, int page, double pageResult);
    }
}
