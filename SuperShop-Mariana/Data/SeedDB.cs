using SuperShop_Mariana.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class SeedDB
    {
        private readonly DataContext _context; //Poder construir as coisas

        private Random _random; //gerar produtos aleatoriamente.
        public SeedDB(DataContext context)
        {
            _context = context;
            _random = new Random(); 
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //vai criar a base de dados, se nao tiver criada, ela cria.
            if(!_context.products.Any())
            {
               AddProduct("Iphone X");
                AddProduct("Magic Mickey Mouse");
                AddProduct("iWatch Series 4");
                AddProduct("iPad Mini");
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _context.products.Add(new Products
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvaiable = true,
                Stock = _random.Next(100),
            } );
               
        }
    }
}
