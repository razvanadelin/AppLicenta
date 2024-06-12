using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;

        public AchievementsController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        // GET: api/achievements
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var achievements = await _myProjectDbContext.Achievements.Include(a => a.User).ToListAsync();

            if (achievements.Count == 0)
            {
                return NotFound("Achievements not found");
            }

            return Ok(achievements);
        }

        // GET: api/achievements/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAchievementByIdAsync(int id)
        {
            var achievement = await _myProjectDbContext.Achievements.Include(a => a.User)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(a => a.RealizareId == id);

            if (achievement == null)
            {
                return NotFound("Achievement not found");
            }

            return Ok(achievement);
        }

        // POST: api/achievements
        [HttpPost]
        public async Task<IActionResult> PostAsync(Achievements achievement)
        {
            if (achievement == null)
            {
                return BadRequest("Request is incorrect");
            }

            // Verifică dacă utilizatorul există
            var existingUser = await _myProjectDbContext.User.FindAsync(achievement.UserID);
            if (existingUser == null)
            {
                return BadRequest("User does not exist");
            }

            // Asociază utilizatorul existent cu realizarea
            achievement.User = existingUser;

            _myProjectDbContext.Achievements.Add(achievement);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/api/achievements/{achievement.RealizareId}", achievement);
        }

        // PUT: api/achievements/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Achievements achievementToUpdate)
        {
            if (id != achievementToUpdate.RealizareId)
            {
                return BadRequest("Achievement ID mismatch");
            }

            var existingAchievement = await _myProjectDbContext.Achievements.AsNoTracking()
                                                    .FirstOrDefaultAsync(a => a.RealizareId == id);
            if (existingAchievement == null)
            {
                return NotFound("Achievement not found");
            }

            _myProjectDbContext.Entry(achievementToUpdate).State = EntityState.Modified;
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/achievements/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var achievement = await _myProjectDbContext.Achievements.FindAsync(id);
            if (achievement == null)
            {
                return NotFound("Achievement not found");
            }

            _myProjectDbContext.Achievements.Remove(achievement);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
