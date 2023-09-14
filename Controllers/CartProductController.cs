using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using veebipoe.Data;
using veebipoe.Models;

namespace veebipoe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CartProduct> GetCartProducts()
        {
            var CartProducts = _context.CartProduct.ToList();
            return CartProducts;
        }

        [HttpPost]
        public ActionResult<List<CartProduct>> PostCartProduct([FromBody] CartProduct CartProduct)
        {
            _context.CartProduct.Add(CartProduct);
            _context.SaveChanges();


            return Ok(_context.CartProduct);
        }

        [HttpDelete("{id}")]
        public List<CartProduct> DeleteCartProduct(int id)
        {
            var CartProduct = _context.CartProduct.Find(id);

            if (CartProduct == null)
            {
                return _context.CartProduct.ToList();
            }

            _context.CartProduct.Remove(CartProduct);
            _context.SaveChanges();
            return _context.CartProduct.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCartProduct(int id)
        {
            var CartProduct = _context.CartProduct.Find(id);

            if (CartProduct == null)
            {
                return NotFound();
            }

            return CartProduct;
        }

        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCartProduct(int id, [FromBody] CartProduct updatedCartProduct)
        {
            var CartProduct = _context.CartProduct.Find(id);

            if (CartProduct == null)
            {
                return NotFound();
            }

            CartProduct.Quantity = updatedCartProduct.Quantity;

            _context.CartProduct.Update(CartProduct);
            _context.SaveChanges();

            return Ok(_context.CartProduct);
        }

        [HttpGet("{orderId}")]
        public ActionResult<List<CartProduct>> GetCartProductsForOrder(int orderId)
        {
            var CartProducts = _context.CartProduct.Where(c => c.OrderId == orderId).ToList();

            if (CartProducts.Count == 0)
            {
                return NotFound();
            }

            return CartProducts;
        }

        [HttpGet("{productId}")]
        public ActionResult<List<CartProduct>> GetCartProductsForProduct(int productId)
        {
            var CartProducts = _context.CartProduct.Where(c => c.ProductId == productId).ToList();

            if (CartProducts.Count == 0)
            {
                return NotFound();
            }

            return CartProducts;
        }
    }
}
