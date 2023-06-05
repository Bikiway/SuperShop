using SuperShop_Mariana.Data.Entities;
using System.Linq;

namespace SuperShop_Mariana.Data
{
    public interface IProductsRepository : IGenericRepository<Products>
    {
        public IQueryable GetAllWithUsers();
    }
}
