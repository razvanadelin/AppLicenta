using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public TrainingController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/Training
        [HttpGet]
        public async Task<IActionResult> GetTrainings()
        {
            var trainings = await _context.Training.Include(t => t.ExerciseTraining).ThenInclude(et => et.Exercices).ToListAsync();

            if (trainings.Count == 0)
            {
                return NotFound("Trainings not found");
            }

            return Ok(trainings);
        }

        [HttpGet("generate")]
        public async Task<IActionResult> GenerateTrainingPlan(int userId, string nivelFitness, string scopAntrenament)
        {
            var training = await _context.Training
                .Include(t => t.ExerciseTraining)
                .ThenInclude(et => et.Exercices)
                .FirstOrDefaultAsync(t => t.UserID == userId && t.NivelFitness == nivelFitness && t.ScopAntrenament == scopAntrenament);

            if (training == null)
            {
                return NotFound("No matching training plan found");
            }

            // Verifică și asigură-te că ExerciseTraining este un array
            if (training.ExerciseTraining == null)
            {
                training.ExerciseTraining = new List<ExerciseTraining>();
            }

            return Ok(training);
        }


        // GET: api/Training/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var training = await _context.Training.Include(t => t.ExerciseTraining).ThenInclude(et => et.Exercices)
                                                  .FirstOrDefaultAsync(t => t.ProgramID == id);

            if (training == null)
            {
                return NotFound("Training not found");
            }

            return Ok(training);
        }

        // POST: api/Training
        [HttpPost]
        public async Task<IActionResult> PostTraining([FromBody] Training training)
        {
            if (training == null)
            {
                return BadRequest("Invalid training data");
            }

            _context.Training.Add(training);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTrainingById), new { id = training.ProgramID }, training);
        }

        // PUT: api/Training/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraining(int id, [FromBody] Training training)
        {
            if (id != training.ProgramID)
            {
                return BadRequest("Training ID mismatch");
            }

            var existingTraining = await _context.Training.FindAsync(id);
            if (existingTraining == null)
            {
                return NotFound("Training not found");
            }

            existingTraining.DataIncepere = training.DataIncepere;
            existingTraining.DataSf = training.DataSf;
            existingTraining.NivelFitness = training.NivelFitness;
            existingTraining.ScopAntrenament = training.ScopAntrenament;

            _context.Training.Update(existingTraining);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Training/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining(int id)
        {
            var training = await _context.Training.FindAsync(id);
            if (training == null)
            {
                return NotFound("Training not found");
            }

            _context.Training.Remove(training);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
