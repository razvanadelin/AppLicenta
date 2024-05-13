using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTrainingController : Controller
    {
        private readonly ProjectDbContext _myProjectDbContext;
        public ExerciseTrainingController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var ExerciseTraining = await _myProjectDbContext.ExerciseTraining.ToListAsync();

            if (ExerciseTraining.Count == 0)
            {
                return NotFound("ExerciseTraining not found");
            }

            return Ok(ExerciseTraining);
        }
    }
}
