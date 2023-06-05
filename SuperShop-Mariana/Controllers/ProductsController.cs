using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SuperShop_Mariana.Data;
using SuperShop_Mariana.Data.Entities;
using SuperShop_Mariana.Helpers;
using SuperShop_Mariana.Models;

namespace SuperShop_Mariana.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _repository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;
        public ProductsController(IProductsRepository repository, IUserHelper userHelper, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _repository = repository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
              return View(_repository.GetAllWithUser().OrderBy(e => e.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Quando tiver valor nulo, passa a um valor dele próprio e não rebenta.
            var products = await _repository.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");
                }

                 //products = this.ToProducts(model,path);
               var products = _converterHelper.ToProducts(model, path, true);
                //Logar o produto
                products.user = await _userHelper.GetUserByEmailAsync("mariana.95@outlook.pt");
                await _repository.CreateAsync(products);
                //Não é preciso gravar, pois já gravou no repositório. Redundancia...
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //private Products ToProducts(ProductViewModel model, string path)
        //{
        //    return new Products
        //    {
        //        Id = model.Id,
        //        Name = model.Name,
        //        ImageUrl = path,
        //        IsAvaiable = model.IsAvaiable,
        //        LastPurchase = model.LastPurchase,
        //        LastSale = model.LastSale,
        //        Price = model.Price,
        //        Stock = model.Stock,
        //        user = model.user,
        //    };
        //}

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _repository.GetByIdAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToProductViewModel(products);
            return View(model);
        }

        //private object ToProductViewModel(Products products)
        //{
        //    return new ProductViewModel
        //    {
        //        Id = products.Id,
        //        Name = products.Name,
        //        IsAvaiable = products.IsAvaiable,
        //        LastPurchase = products.LastPurchase,
        //        LastSale = products.LastSale,
        //        Price = products.Price,
        //        Stock = products.Stock,
        //        user = products.user,
        //        ImageUrl = products.ImageUrl,
        //    };
        //}

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = model.ImageUrl; //para o caso de não alterar a imagem.

                    if(model.ImageFile != null && model.ImageFile.Length > 0) 
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "products");
                    }
                    //var product = this.ToProducts(model, path);
                    var product = _converterHelper.ToProducts(model, path, false);

                    product.user = await _userHelper.GetUserByEmailAsync("mariana.95@outlook.pt");
                    await _repository.UpdateAsync(product);
                    //await _repository.SaveAll();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _repository.ExistAsync(model.Id))
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
            return View(model);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _repository.GetByIdAsync(id.Value);
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
            var products = await _repository.GetByIdAsync(id);
            if (products != null)
            {
                await _repository.DeleteAsync(products);
            }
            
            //await _repository.SaveAll();
            return RedirectToAction(nameof(Index));
        }
    }
}
