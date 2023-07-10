using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace SuperShop_Mariana.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

    }
}
