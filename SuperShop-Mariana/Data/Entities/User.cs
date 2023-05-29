using Microsoft.AspNetCore.Identity;
namespace SuperShop_Mariana.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}
