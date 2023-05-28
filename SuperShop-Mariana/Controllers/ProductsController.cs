using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data;
using SuperShop_Mariana.Data.Entities;

namespace SuperShop_Mariana.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository _repository;

        public ProductsController(IRepository repository)
        {
            this._repository = repository;
        }

        // GET: Products
        public IActionResult Index()
        {
              return View(_repository.GetProducts());
        }

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Quando tiver valor nulo, passa a um valor dele próprio e não rebenta.
            var products = _repository.GetProduct(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvaiable,Stock")] Products products)
        {
            if (ModelState.IsValid)
            {
                _repository.AddProduct(products);
                await _repository.SaveAll();
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _repository.GetProduct(id.Value);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageUrl,LastPurchase,LastSale,IsAvaiable,Stock")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateProduct(products);
                    await _repository.SaveAll();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_repository.ProductExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = _repository.GetProduct(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_repository == null)
            {
                return Problem("Entity set 'DataContext.products'  is null.");
            }
            var products = _repository.GetProduct(id);
            if (products != null)
            {
                _repository.RemoveProduct(products);
            }
            
            await _repository.SaveAll();
            return RedirectToAction(nameof(Index));
        }
    }
}
