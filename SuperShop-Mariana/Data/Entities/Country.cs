using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Data.Entities
{
    public class Country : IEntenty
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} can contain {1} characters.")]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        [Display(Name ="Number os cities")]
        public int NumberCities => Cities == null ? 0 : Cities.Count; //Só leitura, que conta o número de cidades.
    }
}
