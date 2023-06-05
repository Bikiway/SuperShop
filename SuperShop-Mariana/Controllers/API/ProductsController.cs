using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShop_Mariana.Data;

namespace SuperShop_Mariana.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public IActionResult GetProducts() 
        {
            return Ok(_productsRepository.GetAllWithUsers()); //Agarra tudo e embrulha no Json.
        }

    }
}
