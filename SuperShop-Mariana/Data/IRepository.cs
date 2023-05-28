using SuperShop_Mariana.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public interface IRepository
    {
        void AddProduct(Products products);
        Products GetProduct(int id);
        IEnumerable<Products> GetProducts();
        bool ProductExists(int id);
        void RemoveProduct(Products products);
        Task<bool> SaveAll();
        void UpdateProduct(Products products);
    }
}