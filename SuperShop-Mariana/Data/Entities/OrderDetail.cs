using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Data.Entities
{
    public class OrderDetail : IEntenty
    {
        public int Id { get; set; }

        [Required]
        public Products products { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }


        public decimal Value => Price * (decimal)Quantity;
    }
}
