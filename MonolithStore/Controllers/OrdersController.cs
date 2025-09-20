using Microsoft.AspNetCore.Mvc;
using MonolithStore.DTOs;
using MonolithStore.Models;
using MonolithStore.Services;

namespace MonolithStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IDataStore _store;
        public OrdersController(IDataStore store) => _store = store;

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll() => Ok(_store.Orders);

        [HttpGet("{id:int}")]
        public ActionResult<Order> Get(int id)
        {
            var order = _store.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody] CreateOrderDto dto)
        {
            if (dto == null || !dto.Items.Any()) return BadRequest("Order must contain at least one item.");

            var items = new List<OrderItem>();
            foreach (var i in dto.Items)
            {
                var prod = _store.GetProduct(i.ProductId);
                if (prod == null) return BadRequest($"Product id {i.ProductId} not found.");
                if (i.Quantity <= 0) return BadRequest("Quantity must be > 0.");
                // NOTE: This store does not change product stock; it's a read-only product list per requirement.
                items.Add(new OrderItem
                {
                    ProductId = prod.ProductId,
                    ProductName = prod.Name,
                    UnitPrice = prod.Price,
                    Quantity = i.Quantity
                });
            }

            var order = new Order { Items = items };
            var created = _store.CreateOrder(order);
            return CreatedAtAction(nameof(Get), new { id = created.OrderId }, created);
        }
    }
}
