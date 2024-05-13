using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : Controller
    {
        private readonly ProjectDbContext _myProjectDbContext;
        public AchievementsController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var Achievements = await _myProjectDbContext.Achievements.ToListAsync();

            if (Achievements.Count == 0)
            {
                return NotFound("Achievements not found");
            }

            return Ok(Achievements);
        }
    }
}
