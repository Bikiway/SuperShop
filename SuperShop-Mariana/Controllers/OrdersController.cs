using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperShop_Mariana.Data;
using SuperShop_Mariana.Models;
using System;
using System.Threading.Tasks;

namespace SuperShop_Mariana.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductsRepository _productsRepository;

        public OrdersController(IOrderRepository orderRepository, IProductsRepository productsRepository)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;

        }


        public async Task<IActionResult> Index()
        {
            var model = await _orderRepository.GetOrderAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _orderRepository.GetDetailsTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new AddItemViewModel
            {
                Quantity = 1,
                Products = _productsRepository.GetComboProducts()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return RedirectToAction("Create");
            }

            return View(model);
        }


        public async Task<IActionResult> DeleteItem(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            await _orderRepository.DeleteTempAsync(Id.Value);
            return RedirectToAction("Create"); //Atualiza o create, senão não desaparece da lista.
        }

        public async Task<IActionResult> Increase(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailsTempQuantityAsync(Id.Value, 1);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            await _orderRepository.ModifyOrderDetailsTempQuantityAsync(Id.Value, -1);
            return RedirectToAction("Create");
        }


        public async Task<IActionResult> ConfirmOrder()
        {
            var response = await _orderRepository.ConfirmOrderAsync(this.User.Identity.Name);

            if(response)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Deliver(int? id)
        {
            if(id == null) 
            {
                return NotFound();
            }

            var order = await _orderRepository.GetOderAsync(id.Value);

            if(order == null)
            {
                return NotFound();
            }

            var model = new DeliveryViewModel //vai chamar a action
            {
                Id = order.Id,
                DeliveryDate = DateTime.Today
            };

            return View(model);

            //Faz aparecer a view
        }

        [HttpPost]
        public async Task<IActionResult> Deliver(DeliveryViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _orderRepository.DeliveryOder(model);
                return RedirectToAction("Index"); //Index do próprio controlador
            }

            return View();
        }
    }
}
