using SuperShop_Mariana.Data.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class Repository : IRepository
    {
        // vai aceder ao DataContext. vai injetar o datacontext

        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetProducts()
        {
            //Método que me dá todos os produtos
            return _context.products.OrderBy(p => p.Name);
        }

        //Retorna só um produto
        public Products GetProduct(int id)
        {
            return _context.products.Find(id);
        }

        //Create product
        public void AddProduct(Products products)
        {
            _context.products.Add(products); //Adicionado na memória e não na base de dados
        }

        public void UpdateProduct(Products products)
        {
            _context.products.Update(products); //Update products
        }

        public void RemoveProduct(Products products)
        {
            _context.products.Remove(products);
        }

        //assincrono
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; //Grava tudo o que está pendente para a base de dados. Isto chamado no final.
        }

        public bool ProductExists(int id)
        {
            return _context.products.Any(p => p.Id == id); //Se existe.
        }

    }
}
