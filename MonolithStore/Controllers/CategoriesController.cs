using Microsoft.AspNetCore.Mvc;
using MonolithStore.DTOs;
using MonolithStore.Services;

namespace MonolithStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataStore _store;
        public CategoriesController(IDataStore store) => _store = store;

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAll()
        {
            var result = _store.Categories.Select(c => new CategoryDto(c.CategoryId, c.Name));
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var c = _store.GetCategory(id);
            if (c == null) return NotFound();
            return Ok(new CategoryDto(c.CategoryId, c.Name));
        }

        [HttpGet("{id:int}/products")]
        public ActionResult GetProducts(int id)
        {
            var category = _store.GetCategory(id);
            if (category == null) return NotFound();
            var prods = _store.GetProductsByCategory(id)
                .Select(p => new DTOs.ProductDto(p.ProductId, p.Name, p.Description, p.Price, p.CategoryId, p.Stock));
            return Ok(prods);
        }
    }
}
