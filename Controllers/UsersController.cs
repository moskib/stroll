using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stroll.Data;
using Stroll.Models;
using Stroll.Services;

namespace Stroll.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public UsersController(ApplicationDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAllAsync();
                return Ok(users);

            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                // TODO: log exception
                return StatusCode(500, "Internal server error");
            }
        }
        

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.UID)
            {
                return BadRequest();
            }

            _unitOfWork.Users.Update(user);

            try
            {
                await _unitOfWork.SaveChangesAsync();
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _unitOfWork.Users.Exists(e => e.UID == id);
        }
    }
}
