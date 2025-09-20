using Microsoft.AspNetCore.Mvc;
using MonolithStore.DTOs;
using MonolithStore.Services;

namespace MonolithStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IDataStore _store;
        public ProductsController(IDataStore store) => _store = store;

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var result = _store.Products.Select(p => new ProductDto(p.ProductId, p.Name, p.Description, p.Price, p.CategoryId, p.Stock));
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductDto> Get(int id)
        {
            var p = _store.GetProduct(id);
            if (p == null) return NotFound();
            return Ok(new ProductDto(p.ProductId, p.Name, p.Description, p.Price, p.CategoryId, p.Stock));
        }
    }
}
