using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseTrainingController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;

        public ExerciseTrainingController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        // GET: api/exercisetraining
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var exerciseTrainings = await _myProjectDbContext.ExerciseTraining
                                    .Include(et => et.Training)
                                    .Include(et => et.Exercices)
                                    .ToListAsync();

            if (exerciseTrainings.Count == 0)
            {
                return NotFound("ExerciseTrainings not found");
            }

            return Ok(exerciseTrainings);
        }

        // GET: api/exercisetraining/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseTrainingByIdAsync(int id)
        {
            var exerciseTraining = await _myProjectDbContext.ExerciseTraining
                                        .Include(et => et.Training)
                                        .Include(et => et.Exercices)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(et => et.ExerciseTrainingID == id);

            if (exerciseTraining == null)
            {
                return NotFound("ExerciseTraining not found");
            }

            return Ok(exerciseTraining);
        }

        // POST: api/exercisetraining
        [HttpPost]
        public async Task<IActionResult> PostAsync(ExerciseTraining exerciseTraining)
        {
            if (exerciseTraining == null)
            {
                return BadRequest("Request is incorrect");
            }

            var training = await _myProjectDbContext.Training.FindAsync(exerciseTraining.ProgramID);
            if (training == null)
            {
                return NotFound("Training not found");
            }

            var exercice = await _myProjectDbContext.Exercices.FindAsync(exerciseTraining.ExID);
            if (exercice == null)
            {
                return NotFound("Exercice not found");
            }

            // Asociază training-ul și exercițiul existent cu programul de exerciții
            exerciseTraining.Training = training;
            exerciseTraining.Exercices = exercice;

            _myProjectDbContext.ExerciseTraining.Add(exerciseTraining);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/api/exercisetraining/{exerciseTraining.ExerciseTrainingID}", exerciseTraining);
        }

        // PUT: api/exercisetraining/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, ExerciseTraining exerciseTrainingToUpdate)
        {
            if (id != exerciseTrainingToUpdate.ExerciseTrainingID)
            {
                return BadRequest("ExerciseTraining ID mismatch");
            }

            var existingExerciseTraining = await _myProjectDbContext.ExerciseTraining.AsNoTracking()
                                                            .FirstOrDefaultAsync(et => et.ExerciseTrainingID == id);
            if (existingExerciseTraining == null)
            {
                return NotFound("ExerciseTraining not found");
            }

            var training = await _myProjectDbContext.Training.FindAsync(exerciseTrainingToUpdate.ProgramID);
            if (training == null)
            {
                return NotFound("Training not found");
            }

            var exercice = await _myProjectDbContext.Exercices.FindAsync(exerciseTrainingToUpdate.ExID);
            if (exercice == null)
            {
                return NotFound("Exercice not found");
            }

            // Asociază training-ul și exercițiul existent cu programul de exerciții
            exerciseTrainingToUpdate.Training = training;
            exerciseTrainingToUpdate.Exercices = exercice;

            _myProjectDbContext.Entry(exerciseTrainingToUpdate).State = EntityState.Modified;
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/exercisetraining/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var exerciseTraining = await _myProjectDbContext.ExerciseTraining.FindAsync(id);
            if (exerciseTraining == null)
            {
                return NotFound("ExerciseTraining not found");
            }

            _myProjectDbContext.ExerciseTraining.Remove(exerciseTraining);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
