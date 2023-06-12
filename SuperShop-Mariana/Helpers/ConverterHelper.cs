using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System;
using System.IO;
using System.Xml.Linq;

namespace SuperShop_Mariana.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Products ToProducts(ProductViewModel model, Guid imageId, bool isNew)
        {
            return new Products
            {
                Id = isNew? 0 : model.Id,
                Name = model.Name,
                ImageId = imageId,
                IsAvaiable = model.IsAvaiable,
                LastPurchase = model.LastPurchase,
                LastSale = model.LastSale,
                Price = model.Price,
                Stock = model.Stock,
                user = model.user,
            };
         
        }

        public ProductViewModel ToProductViewModel(Products products)
        {
            return new ProductViewModel
            {
                Id = products.Id,
                Name = products.Name,
                IsAvaiable = products.IsAvaiable,
                LastPurchase = products.LastPurchase,
                LastSale = products.LastSale,
                Price = products.Price,
                Stock = products.Stock,
                user = products.user,
                ImageId = products.ImageId,
            };
        }
    }
}
