using Microsoft.AspNetCore.Mvc;
using SuperShop_Mariana.Helpers;
using SuperShop_Mariana.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public IActionResult Login() //Só na view
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home"); //Primeiro a action e depois o controlador.
            }
            return View(); //Caso algo não corra bem.
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var result = await _userHelper.LoginAsync(model);

                if (result.Succeeded) 
                {
                    if(this.Request.Query.Keys.Contains("ReturnUrl")) //Url de retorno: 
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Failed to login...");
            return View(model); //Fica no mesmo sitio, sem limpar.
        }


        public async Task<IActionResult> LogOut()
        {
            await _userHelper.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
