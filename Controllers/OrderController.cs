using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipoe.Data;
using veebipoe.Models;

namespace veebipoe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Order> GetOrders()
        {
            var orders = _context.Order.ToList();
            return orders;
        }

        [HttpPost]
        public ActionResult<List<Order>> PostOrder([FromBody] Order order)
        {
            order.created = DateTime.Now;
            _context.Order.Add(order);
            _context.SaveChanges();

            return Ok(_context.Order);
        }

        [HttpDelete("{id}")]
        public List<Order> DeleteOrder(int id)
        {
            var order = _context.Order.Find(id);

            if (order == null)
            {
                return _context.Order.ToList();
            }

            _context.Order.Remove(order);
            _context.SaveChanges();
            return _context.Order.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _context.Order.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedorder)
        {
            var order = _context.Order.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            order.TotalSum = updatedorder.TotalSum;
            order.Paid = updatedorder.Paid;

            _context.Order.Update(order);
            _context.SaveChanges();

            return Ok(_context.Order);
        }

        [HttpGet("{personId}")]
        public ActionResult<List<Order>> GetOrdersForPerson(int personId)
        {
            var orders = _context.Order.Where(c => c.PersonId == personId).ToList();

            if (orders.Count == 0)
            {
                return NotFound();
            }

            return orders;
        }
    }
}
