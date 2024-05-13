using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : Controller
    {
        private readonly ProjectDbContext _myProjectDbContext;
        public TrainingController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var Trainings = await _myProjectDbContext.Training.ToListAsync();

            if (Trainings.Count == 0)
            {
                return NotFound("Trainings not found");
            }

            return Ok(Trainings);
        }
    }
}
