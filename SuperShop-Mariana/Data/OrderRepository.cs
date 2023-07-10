using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Data
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context) 
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return null;
            }

            if(await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders //Aceder a várias tabelas ao mesmo tempo. Include == InnerJoin e tabela diretas. ThenInclude (E incluí também) é para incluir outra tabelas. 
                    .Include(o => o.Items)
                    .ThenInclude(i => i.products)
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Orders
                .Include(o=>o.Items)
                .ThenInclude(i => i.products)
                .Where(o => o.user == user)
                .OrderByDescending (o => o.OrderDate);
        }

        
    }
}
