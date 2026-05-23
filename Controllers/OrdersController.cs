using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<Order> orders)
        {
            foreach (var order in orders)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == order.ProductName);

                if (product != null)
                {
                    product.Quantity -= order.Quantity;

                    if (product.Quantity < 0)
                    {
                        return BadRequest("Not enough stock");
                    }
                }
            }

            await _context.Orders.AddRangeAsync(orders);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_context.Orders.ToList());
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrderStatus(int id,[FromBody] string status)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;

            _context.SaveChanges();

            return Ok(order);
        }
    }
}
