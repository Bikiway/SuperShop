using System.ComponentModel.DataAnnotations;
using System;

namespace SuperShop_Mariana.Models
{
    public class DeliveryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }
    }
}
