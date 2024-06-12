using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;

        public UserController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _myProjectDbContext.User.ToListAsync();

            if (users.Count == 0)
            {
                return NotFound("Users not found");
            }

            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await _myProjectDbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
        // POST: api/user/signup
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {
            var existingUser = await _myProjectDbContext.User.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (existingUser != null)
            {
                return Conflict("Username already exists");
            }

            _myProjectDbContext.User.Add(user);
            await _myProjectDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserByIdAsync), new { id = user.UserID }, user);
        }
        // POST: api/user/signin
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] UserLogin userLogin)
        {
            var user = await _myProjectDbContext.User.FirstOrDefaultAsync(u => u.Username == userLogin.Username && u.Password == userLogin.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(user);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, User userToUpdate)
        {
            if (id != userToUpdate.UserID)
            {
                return BadRequest("User ID mismatch");
            }

            var existingUser = await _myProjectDbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.UserID == id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            _myProjectDbContext.Entry(userToUpdate).State = EntityState.Modified;
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var user = await _myProjectDbContext.User.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            _myProjectDbContext.User.Remove(user);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
