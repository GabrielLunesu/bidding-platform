using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bidding_platform.Data;
using bidding_platform.Models;

namespace bidding_platform.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Products.Include(p => p.User);
            return View(await appDbContext.ToListAsync());
        }

        // OG: GET: Product/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var product = await _context.Products
        //         .Include(p => p.User)
        //         .FirstOrDefaultAsync(m => m.ProductId == id);
        //     if (product == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(product);
        // }

        // Display product details with bids
        [HttpGet("Product/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.User) // Include the User who created the product
                .Include(p => p.Bids)
                    .ThenInclude(b => b.User) // Include Users who placed bids
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,StartingPrice,StartDate,EndDate,BidIncrement,UserId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", product.UserId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", product.UserId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProductId,Name,Description,StartingPrice,StartDate,EndDate,BidIncrement,UserId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", product.UserId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int? id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


        // Handle bid submission
        [HttpPost("Product/PlaceBid")]
        public async Task<IActionResult> PlaceBid(int ProductId, double Amount, int UserId)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(ProductId);

                if (product == null)
                {
                    return NotFound();
                }


                if (product.EndDate < DateTime.Now)
                {
                    ModelState.AddModelError("End Date Expired", "Bid submission period has ended");
                    // return same view
                    return RedirectToAction("Details", product);
                }

                var newBid = new Bid
                {
                    Amount = Amount,
                    BidDate = DateTime.Now,
                    ProductId = ProductId,
                    UserId = UserId,
                };

                _context.Bids.Add(newBid);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = ProductId });
            }

            return BadRequest("Invalid bid submission");
        }
    }
}
