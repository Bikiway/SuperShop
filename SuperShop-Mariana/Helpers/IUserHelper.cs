using Microsoft.AspNetCore.Identity;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);//Vai receber o user que quero criar e a password

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogOutAsync();

        Task<IdentityResult> UpdateUSerAsync(User user); //Update o user first name and last

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword); //Mudar a pass, compará-la com a antiga e substituir.
    }
}
