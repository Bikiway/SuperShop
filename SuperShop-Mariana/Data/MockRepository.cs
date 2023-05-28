using SuperShop_Mariana.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class MockRepository : IRepository
    {
        void IRepository.AddProduct(Products products)
        {
            throw new System.NotImplementedException();
        }

        Products IRepository.GetProduct(int id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Products> IRepository.GetProducts()
        {
            //Pode-se trocar por uma base de dados e fazer testes com vários.
            var products = new List<Products>();
            products.Add(new Products { Id = 1, Name = "Um", Price = 10 });
            products.Add(new Products { Id = 2, Name = "Dois", Price = 20 });
            products.Add(new Products { Id = 3, Name = "Três", Price = 30 });
            products.Add(new Products { Id = 4, Name = "Quatro", Price = 40 });

            return products;
        }

        bool IRepository.ProductExists(int id)
        {
            throw new System.NotImplementedException();
        }

        void IRepository.RemoveProduct(Products products)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IRepository.SaveAll()
        {
            throw new System.NotImplementedException();
        }

        void IRepository.UpdateProduct(Products products)
        {
            throw new System.NotImplementedException();
        }
    }
}
