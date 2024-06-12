using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercicesController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;

        public ExercicesController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        // GET: api/exercices
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var exercices = await _myProjectDbContext.Exercices.Include(e => e.ExerciseTraining).ToListAsync();

            if (exercices.Count == 0)
            {
                return NotFound("Exercices not found");
            }

            return Ok(exercices);
        }

        // GET: api/exercices/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciceByIdAsync(int id)
        {
            var exercice = await _myProjectDbContext.Exercices.Include(e => e.ExerciseTraining)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(e => e.ExID == id);

            if (exercice == null)
            {
                return NotFound("Exercice not found");
            }

            return Ok(exercice);
        }

        // POST: api/exercices
        [HttpPost]
        public async Task<IActionResult> PostAsync(Exercices exercice)
        {
            if (exercice == null)
            {
                return BadRequest("Request is incorrect");
            }

            _myProjectDbContext.Exercices.Add(exercice);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/api/exercices/{exercice.ExID}", exercice);
        }

        // PUT: api/exercices/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Exercices exerciceToUpdate)
        {
            if (id != exerciceToUpdate.ExID)
            {
                return BadRequest("Exercice ID mismatch");
            }

            var existingExercice = await _myProjectDbContext.Exercices.AsNoTracking()
                                                    .FirstOrDefaultAsync(e => e.ExID == id);
            if (existingExercice == null)
            {
                return NotFound("Exercice not found");
            }

            _myProjectDbContext.Entry(exerciceToUpdate).State = EntityState.Modified;
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/exercices/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exercice = await _myProjectDbContext.Exercices.FindAsync(id);
            if (exercice == null)
            {
                return NotFound("Exercice not found");
            }

            _myProjectDbContext.Exercices.Remove(exercice);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
