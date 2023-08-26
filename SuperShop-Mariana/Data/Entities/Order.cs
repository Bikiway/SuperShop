using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SuperShop_Mariana.Data.Entities
{
    public class Order : IEntenty
    {
        public int Id { get; set; }


        [Required]
        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime OrderDate { get; set; }


        [Required]
        [Display(Name = "Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }


        [Required]
        public User user { get; set; }


        public IEnumerable<OrderDetail> Items { get; set; } //Lista de order details. Ligação de um para muitos.

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity); //Caso items == null, somamos. Caso null mete 0.


        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);
    }
}
