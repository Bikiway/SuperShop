using SuperShop_Mariana.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName); //Tarefa que devolve uma tabela de order//Dá-me todas as encomendas de um determinado user.
    }
}
