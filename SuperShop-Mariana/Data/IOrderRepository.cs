using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Models;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrderAsync(string userName); //Tarefa que devolve uma tabela de order//Dá-me todas as encomendas de um determinado user.

        Task<IQueryable<OrderDetailTmp>> GetDetailsTempsAsync(string userName); //Dá os users temporários

        Task AddItemToOrderAsync(AddItemViewModel model, string userName); //Add items

        Task ModifyOrderDetailsTempQuantityAsync(int Id, double quantity); //Modifica os items que lá estão.

        Task DeleteTempAsync(int Id);

        Task<bool> ConfirmOrderAsync(string userName);

        Task DeliveryOder(DeliveryViewModel model);

        Task<Order> GetOderAsync(int id); //Método que recebe o id e me dê uma encomenda através do id.
    }
}
