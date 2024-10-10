using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using bidding_platform.Data;
using bidding_platform.Models;
using bidding_platform.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bidding_platform.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BidAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/BidAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids()
        {
            return await _context.Bids.ToListAsync();
        }

        // GET: api/BidAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _context.Bids.FindAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return bid;
        }

        // POST: api/BidAPI
        // STILL HAVE TO FIX THE BID INCREMENT HERE!
        [HttpPost]
        public async Task<ActionResult<Bid>> CreateBid(CreateBidViewModel model)
        {
            var bid = new Bid
            {
                Amount = model.Amount,
                BidDate = DateTime.UtcNow,
                UserId = model.UserId,
                ProductId = model.ProductId
            };

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBid), new { id = bid.BidId }, bid);
        }

        // PUT: api/BidAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBid(int id, UpdateBidViewModel model)
        {
            if (id != model.BidId)
            {
                return BadRequest();
            }

            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            bid.Amount = model.Amount;
            // Note: Typically, you wouldn't update the BidDate, UserId, or ProductId after creation

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidExists(id))
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

        // DELETE: api/BidAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid == null)
            {
                return NotFound();
            }

            _context.Bids.Remove(bid);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidId == id);
        }
    }
}
