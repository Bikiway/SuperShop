using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;

namespace SuperShop_Mariana.Helpers
{
    public interface IConverterHelper
    {
        Products ToProducts(ProductViewModel model, string path, bool isNew);

        ProductViewModel ToProductViewModel(Products products);
    }
}
