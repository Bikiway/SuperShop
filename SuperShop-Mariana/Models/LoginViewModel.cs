using System.ComponentModel.DataAnnotations;

namespace SuperShop_Mariana.Models
{
    public class LoginViewModel
    {
        //Só em termos de view.

        [Required]
        [EmailAddress]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } //Para a pessoa não estar sempre a fazer o login
    }
}
