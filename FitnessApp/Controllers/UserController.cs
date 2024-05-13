using FitnessApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;
        private object _context;

        public UserController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var users = await _myProjectDbContext.User.ToListAsync();

            if (users.Count == 0)
            {
                return NotFound("User not found");
            }

            return Ok(users);
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipmentByIdAsync(int id)
        {
            var actualTask = await _myProjectDbContext.User.AsNoTracking().Where(ats => ats.UserID == id).FirstOrDefaultAsync();

            if (actualTask == null)
            {
                return NotFound("User not found");
            }

            return Ok(actualTask);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(User user)
        {
            if (user == null)
            {
                return BadRequest("Request is incorrect");
            }
            _myProjectDbContext.User.Add(user);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/id?id={user.UserID}", user);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, User actualTaskToUpdate)
        {
            var actualTask = await _myProjectDbContext.User.AsNoTracking().Where(ats => ats.UserID == id).FirstOrDefaultAsync();

            if (actualTask == null)
            {
                return NotFound();
            }
            _myProjectDbContext.User.Update(actualTaskToUpdate);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var actualTask = await _myProjectDbContext.User.FindAsync(id);
            if (actualTask == null)
            {
                return NotFound();
            }
            _myProjectDbContext.User.Remove(actualTask);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        } 

    }
}
