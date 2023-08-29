using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperShop_Mariana.Data;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using SuperShop_Mariana.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IConfiguration _configuration;
        public AccountController(IUserHelper userHelper, ICountryRepository countryRepository, IConfiguration configuration)
        {
            _userHelper = userHelper;
            _countryRepository = countryRepository;
            _configuration = configuration;
        }

        public IActionResult Login() //Só na view
        {
            if (User.Identity.IsAuthenticated)
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
                    if (this.Request.Query.Keys.Contains("ReturnUrl")) //Url de retorno: 
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

        //View account --> Login ---> Register
        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Cities = _countryRepository.GetComboCities(0),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                //criar um user novo.
                if (user == null)
                {
                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.UserName,
                        UserName = model.UserName,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CityId = model.CityId,
                        City = city,
                    };

                    //Caso não consiga criar um login novo
                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    if (result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn't be created.");
                        return View(model);
                    }

                    //construimos o login
                    var loginViewModel = new LoginViewModel
                    {
                        Password = model.Password,
                        RememberMe = false,
                        UserName = model.UserName
                    };

                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    if (result2.Succeeded)
                    {
                        return RedirectToAction("Index", "Home"); //Se conseguir logar
                    }

                    ModelState.AddModelError(string.Empty, "The user couldn't be logged.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;

                var city = await _countryRepository.GetCityAsync(user.CityId);
                if(city != null)
                {
                    var country = await _countryRepository.GetCountryAsync(city);
                    if(country != null)
                    {
                        model.CountryId = country.Id;
                        model.Cities = _countryRepository.GetComboCities(country.Id);
                        model.Countries = _countryRepository.GetComboCountries();
                        model.CityId = user.CityId;
                    }
                }
            }
            model.Cities = _countryRepository.GetComboCities(model.CountryId);
            model.Countries = _countryRepository.GetComboCountries();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CityId = model.CityId;
                    user.City = city;

                    var response = await _userHelper.UpdateUSerAsync(user);

                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult ChangePassword() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                if (user != null)
                {
                    var response = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                    if (response.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found");
                }
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if(this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.UserName);
                if(user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);
                    if(result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])); //Algoritmo para ir buscar a key (No appsettings.json).
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Gerar o token, usando o algoritmo que vem do security key. 256 bits. Depende do middleware.
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }
            return BadRequest();
        }



        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpPost]
        [Route("Account/GetCitiesAsync")] //Caminho para executar a action
        public async Task<JsonResult> GetCitiesAsync(int countryId) //Tudo em formato Json
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
            return this.Json(country.Cities.OrderBy(o => o.Name));
        }
    }
}
