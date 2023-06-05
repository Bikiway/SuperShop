using Microsoft.AspNetCore.Http;
using SuperShop_Mariana.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Models
{
    public class ProductViewModel : Products
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
