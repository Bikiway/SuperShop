using Microsoft.AspNetCore.Identity;
using SuperShop_Mariana.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        public UserHelper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(User user, string password) //Criar um utilizador novo
        {
            return await _userManager.CreateAsync(user, password);
        }

        public Task<IdentityResult> AddUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
