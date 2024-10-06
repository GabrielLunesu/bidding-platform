using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bidding_platform.Data;
using bidding_platform.Models;

namespace bidding_platform.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            var messages = await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();
            return View(messages);
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "Name");
            ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SenderId,RecipientId,Content")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SentDate = DateTime.Now;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SenderId"] = new SelectList(_context.Users, "UserId", "Name", message.SenderId);
            ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "Name", message.RecipientId);
            return View(message);
        }

        // GET: Message/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Recipient)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }
    }
}
