using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;

namespace SuperShop_Mariana.Data
{
    public class DataContext : DbContext //herda a dele
    {
        public DbSet<Products> products { get; set; } //Criar a tabela
        public DataContext(DbContextOptions<DataContext> options) : base(options) //injetamos o options
        {
            
        }
    }
}
