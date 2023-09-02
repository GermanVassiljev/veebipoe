using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using veebipoe.Data;
using veebipoe.Models;

namespace veebipoe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProducts()
        {
            var products = _context.Product.ToList();
            return products;
        }

        [HttpPost]
        public ActionResult<List<Product>> PostProduct([FromBody] Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();

            var category = _context.Category.Include(a => a.Products).SingleOrDefault(a => a.Id == product.CategoryId);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category.Products);
        }

        [HttpDelete("{id}")]
        public List<Product> DeleteProduct(int id)
        {
            var Product = _context.Product.Find(id);

            if (Product == null)
            {
                return _context.Product.ToList();
            }

            _context.Product.Remove(Product);
            _context.SaveChanges();
            return _context.Product.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var Product = _context.Product.Find(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Product;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updatedProduct)
        {
            var Product = _context.Product.Find(id);

            if (Product == null)
            {
                return NotFound();
            }

            Product.Name = updatedProduct.Name;
            Product.Price = updatedProduct.Price;

            _context.Product.Update(Product);
            _context.SaveChanges();

            return Ok(_context.Product);
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<List<Product>> GetProductsForCategory(int categoryId)
        {
            var Products = _context.Product.Where(c => c.CategoryId == categoryId).ToList();

            if (Products.Count == 0)
            {
                return NotFound();
            }

            return Products;
        }
    }
}
