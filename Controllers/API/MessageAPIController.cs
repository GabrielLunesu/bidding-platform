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
    public class MessageAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessageAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MessageAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/MessageAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // POST: api/MessageAPI
        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage(CreateMessageViewModel model)
        {
            var message = new Message
            {
                Content = model.Content,
                SentDate = DateTime.UtcNow,
                SenderId = model.SenderId,
                RecipientId = model.RecipientId
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
        }

        // PUT: api/MessageAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, UpdateMessageViewModel model)
        {
            if (id != model.MessageId)
            {
                return BadRequest();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            message.Content = model.Content;
            // Note: Typically, you wouldn't update the SentDate, SenderId, or RecipientId after creation

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // DELETE: api/MessageAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.MessageId == id);
        }
    }
}
