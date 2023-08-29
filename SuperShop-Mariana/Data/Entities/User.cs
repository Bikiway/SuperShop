using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SuperShop_Mariana.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name ="Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [MaxLength(100, ErrorMessage = "The field {0} can only contain {1} characters.")]
        public string Address { get; set; }

        public int CityId { get; set; } //Só o ID da cidade

        public City City { get; set; } //Objeto da cidade, onde vai buscar as informações

    }
}
