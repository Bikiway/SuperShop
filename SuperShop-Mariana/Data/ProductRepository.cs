using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SuperShop_Mariana.Data
{
    public class ProductRepository : GenericRepository<Products>, IProductsRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base (context) 
        {
           _context = context;
        }  

        public IQueryable GetAllWithUsers() //Query com join. Tipo inner join
        {
            return _context.products.Include(p => p.user);
        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = _context.products.Select(p => new SelectListItem //Agarra em todos os produtos e cria, um a um, um item. 
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Product)",
                Value = "0"
            });

            return list;
        }
    }
}
