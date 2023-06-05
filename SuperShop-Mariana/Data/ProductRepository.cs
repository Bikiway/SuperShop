using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
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
    }
}
