﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Data.Entities
{
    public class Products : IEntenty
    {
        
        public int Id { get; set; }



        [Required] //Obrigatório preencher
        [MaxLength(50, ErrorMessage ="The field {0} can contain {1} characters length.")] //Máximo 50 caracteres(BD) e mensagem de erro(view).
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Image")] //Aparecer na página da web. Só image e não ImageUrl.
        public Guid ImageId { get; set; } 

        
        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; } //? opcional.


        [Display(Name = "Last Sale")]
        public DateTime? LastSale { get; set; }


        [Display(Name="Is Available")]
        public bool IsAvaiable { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }


        public User user { get; set; } //user do produto.Relação de um para muitos.

        public string ImageFullPath => ImageId == Guid.Empty 
            ? $"https://supershop-bikiway.azurewebsites.net/images/products/logotipo.png"
            : $"https://supershope.blob.core.windows.net/products/{ImageId}";
        
            //get
            //{
            //    if (string.IsNullOrEmpty(ImageUrl))
            //    {
            //        return null;
            //    }
            //    return $"https://supershop-bikiway.azurewebsites.net{ImageUrl.Substring(1)}";
            //}


        

    }
}
