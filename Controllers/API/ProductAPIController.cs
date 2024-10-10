using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bidding_platform.Data;
using bidding_platform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using bidding_platform.Models.ViewModels;

namespace bidding_platform.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/ProductAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/ProductAPI
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(CreateProductViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                StartingPrice = model.StartingPrice,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                BidIncrement = model.BidIncrement,
                UserId = model.UserId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/ProductAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductViewModel model)
        {
            if (id != model.ProductId)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.StartingPrice = model.StartingPrice;
            product.StartDate = model.StartDate;
            product.EndDate = model.EndDate;
            product.BidIncrement = model.BidIncrement;
            product.UserId = model.UserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ProductAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
