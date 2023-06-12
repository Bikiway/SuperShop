using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System;

namespace SuperShop_Mariana.Helpers
{
    public interface IConverterHelper
    {
        Products ToProducts(ProductViewModel model, Guid imageId, bool isNew);

        ProductViewModel ToProductViewModel(Products products);
    }
}
