using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercicesController : Controller
    {
        private readonly ProjectDbContext _myProjectDbContext;
        public ExercicesController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var Exercices = await _myProjectDbContext.Exercices.ToListAsync();

            if (Exercices.Count == 0)
            {
                return NotFound("Exercices not found");
            }

            return Ok(Exercices);
        }
    }
}
