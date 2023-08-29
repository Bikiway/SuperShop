using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Models
{
    public class CityViewModel
    {
        public int CountryId { get; set; }

        public int CityID { get; set; }


        [Required]
        [Display(Name ="City Name")]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string CityName { get; set; } 
    }
}
