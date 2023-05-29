using Microsoft.AspNetCore.Identity;
using SuperShop_Mariana.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user); //Vai receber o user que quero criar e a password
        Task<IdentityResult> AddUserAsync(User user, string password);
    }
}
