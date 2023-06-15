using Microsoft.AspNetCore.Identity;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager; //Gestão dos utilizadores
        private readonly SignInManager<User> _signInManager; //Gestão de passwords e users.
        public UserHelper(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password) //Criar um utilizador novo
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
