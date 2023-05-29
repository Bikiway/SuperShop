using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SuperShop_Mariana.Data
{
    public class DataContext : IdentityDbContext<User> //herda a dele
    {
        public DbSet<Products> products { get; set; } //Criar a tabela
        public DataContext(DbContextOptions<DataContext> options) : base(options) //injetamos o options
        {
            
        }
    }
}
