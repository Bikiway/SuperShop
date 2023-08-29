using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using SuperShop_Mariana.Models;
using System;
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

        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName); //Verifica sempre o userName

            if (user == null)
            {
                return;
            }

            var product = await _context.products.FindAsync(model.ProductId);

            if (product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.OrderDetailTemps
                .Where(odt => odt.user == user && odt.products == product)
                .FirstOrDefaultAsync();

            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTmp
                {
                    Price = product.Price,
                    products = product,
                    Quantity = model.Quantity,
                    user = user,
                };

                _context.OrderDetailTemps.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;
                _context.OrderDetailTemps.Update(orderDetailTemp);
            }

            await _context.SaveChangesAsync(); //Gravar no fim na DB.
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            { return false; }

            var orderTemps = await _context.OrderDetailTemps
                .Include(p => p.products)
                .Where(o => o.user == user)
                .ToListAsync();

            if (orderTemps == null || orderTemps.Count == 0)
            {
                return false;
            }

            var details = orderTemps.Select(o => new OrderDetail
            {
                Price = o.Price,
                products = o.products,
                Quantity = o.Quantity,
            }).ToList();

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                user = user,
                Items = details,
            };

            await CreateAsync(order);

            _context.OrderDetailTemps.RemoveRange(orderTemps);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteTempAsync(int Id)
        {
            var orderDetailTemp = await _context.OrderDetailTemps.FindAsync(Id);

            if (orderDetailTemp == null)
            {
                return;
            }

            _context.OrderDetailTemps.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task DeliveryOder(DeliveryViewModel model)
        {
            var order = await _context.Orders.FindAsync(model.Id);

            if (order == null)
            {
                return;
            }

            order.DeliveryDate = model.DeliveryDate;
            _context.Orders.Update(order); //Atualiza na memória, na Entity
            await _context.SaveChangesAsync(); //guardar na BD.
        }

        public async Task<IQueryable<OrderDetailTmp>> GetDetailsTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName); //Verificar o user

            if (user == null)
            {
                return null;
            }

            return _context.OrderDetailTemps
                .Include(p => p.products) //incluí os produtos
                .Where(o => o.user == user) //user == user
                .OrderBy(o => o.products.Name);
        }

        public async Task<Order> GetOderAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Orders //Aceder a várias tabelas ao mesmo tempo. Include == InnerJoin e tabela diretas. ThenInclude (E incluí também) é para incluir outra tabelas. 
                    .Include(o => o.user)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.products)
                    .OrderByDescending(o => o.OrderDate);
            }

            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.products)
                .Where(o => o.user == user)
                .OrderByDescending(o => o.OrderDate);
        }

        public async Task ModifyOrderDetailsTempQuantityAsync(int Id, double quantity)
        {
            var orderDetailsTemp = await _context.OrderDetailTemps.FindAsync(Id);

            if (orderDetailsTemp == null)
            {
                return;
            }

            orderDetailsTemp.Quantity += quantity;

            if (orderDetailsTemp.Quantity > 0)
            {
                _context.OrderDetailTemps.Update(orderDetailsTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
