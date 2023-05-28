using SuperShop_Mariana.Data.Entities;

namespace SuperShop_Mariana.Data
{
    public class ProductRepository : GenericRepository<Products>, IProductsRepository
    {
        public ProductRepository(DataContext context) : base (context) 
        {

        }  
    }
}
