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
    public class UserAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/UserAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/UserAPI
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(CreateUserViewModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email
                // Add other properties as needed
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/UserAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserViewModel model)
        {
            if (id != model.UserId)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Email = model.Email;
            // Update other properties as needed

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // DELETE: api/UserAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
