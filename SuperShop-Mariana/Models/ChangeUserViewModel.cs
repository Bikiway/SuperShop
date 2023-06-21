using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Models
{
    public class ChangeUserViewModel
    {
        //Por questões de segurança a pass fica separada

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
