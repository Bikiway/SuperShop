using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;

namespace SuperShop_Mariana.Data
{
    public class DataContext : IdentityDbContext<User> //herda a dele
    {
        public DbSet<Products> products { get; set; } //Criar a tabela
                                                      
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrdersDetails { get; set; }

        public DbSet<OrderDetailTmp> OrderDetailTemps { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) //injetamos o options
        {

        }
    }
}
