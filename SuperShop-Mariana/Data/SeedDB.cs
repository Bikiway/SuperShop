using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class SeedDB
    {
        private readonly DataContext _context; //Poder construir as coisas

        private Random _random; //gerar produtos aleatoriamente.
                                // private readonly UserManager<User> _userManager;

        private readonly IUserHelper _userHelper;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _random = new Random(); 
            //_userManager = userManager;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync(); //vai criar a base de dados, se nao tiver criada, ela cria.

            await _userHelper.CheckRoleAsync("Admin"); //Se existe esse role admin e customer.
            await _userHelper.CheckRoleAsync("Customer");

            if(!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Aveiro" });
                cities.Add(new City { Name = "Lisboa" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal",
                });

                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("mariana.95@outlook.pt");
            if(user == null)
            {
                user = new User
                {
                    FirstName = "Mariana",
                    LastName = "Oliveira",
                    Email = "mariana.95@outlook.pt",
                    UserName = "mariana.95@outlook.pt",
                    PhoneNumber = "123456789",
                    Address = "Rua Jau 233",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault(),
                };
                var result = await _userHelper.AddUserAsync(user, "1234567"); //Password sempre à parte para depois ser encriptada.

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in Seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin"); //Se o user está no role que queremos, ou não.
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }


            if (!_context.products.Any())
            {
               AddProduct("Iphone X" ,user);
                AddProduct("Magic Mickey Mouse", user);
                AddProduct("iWatch Series 4", user);
                AddProduct("iPad Mini", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.products.Add(new Products
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvaiable = true,
                Stock = _random.Next(100),
                user = user
            }); ;
               
        }
    }
}
