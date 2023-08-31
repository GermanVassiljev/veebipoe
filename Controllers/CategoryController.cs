using Microsoft.AspNetCore.Mvc;
using veebipoe.Data;
using veebipoe.Models;

namespace veebipoe.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> Getcategory()
        {
            var category = _context.Category.ToList();
            return category;
        }

        [HttpPost]
        public List<Category> Postcategory([FromBody] Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
            return _context.Category.ToList();
        }

        [HttpDelete("{id}")]
        public List<Category> Deletecategory(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return _context.Category.ToList();
            }

            _context.Category.Remove(category);
            _context.SaveChanges();
            return _context.Category.ToList();
        }

        [HttpDelete("/kustuta2/{id}")]
        public IActionResult Deletecategory2(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Category> Getcategory(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Category>> Putcategory(int id, [FromBody] Category updatedCategory)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;

            _context.Category.Update(category);
            _context.SaveChanges();

            return Ok(_context.Category);
        }
    }
}
