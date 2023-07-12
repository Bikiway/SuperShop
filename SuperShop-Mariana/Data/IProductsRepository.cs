using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using SuperShop_Mariana.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SuperShop_Mariana.Data
{
    public interface IProductsRepository : IGenericRepository<Products>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboProducts();
    }
}
