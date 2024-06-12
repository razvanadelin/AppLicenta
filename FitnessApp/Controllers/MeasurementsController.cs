using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;

        public MeasurementsController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        // GET: api/measurements
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var measurements = await _myProjectDbContext.Measurements.Include(m => m.User).ToListAsync();

            if (measurements.Count == 0)
            {
                return NotFound("Measurements not found");
            }

            return Ok(measurements);
        }

        // GET: api/measurements/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMeasurementsByUserIdAsync(int userId)
        {
            var measurements = await _myProjectDbContext.Measurements
                                                        .Where(m => m.UserID == userId)
                                                        .ToListAsync();

            if (measurements == null || measurements.Count == 0)
            {
                return NotFound("Measurements not found for the user");
            }

            return Ok(measurements);
        }

        // GET: api/measurements/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeasurementByIdAsync(int id)
        {
            var measurement = await _myProjectDbContext.Measurements.Include(m => m.User)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(m => m.IdMasurare == id);

            if (measurement == null)
            {
                return NotFound("Measurement not found");
            }

            return Ok(measurement);
        }

        // POST: api/measurements
        [HttpPost]
        public async Task<IActionResult> PostAsync(Measurements measurement)
        {
            if (measurement == null)
            {
                return BadRequest("Request is incorrect");
            }

            if (measurement.UserID <= 0)
            {
                return BadRequest("UserID is required");
            }

            var user = await _myProjectDbContext.User.FindAsync(measurement.UserID);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Asociază utilizatorul existent cu măsurarea
            measurement.User = user;

            _myProjectDbContext.Measurements.Add(measurement);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/api/measurements/{measurement.IdMasurare}", measurement);
        }

        // PUT: api/measurements/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Measurements measurementToUpdate)
        {
            if (id != measurementToUpdate.IdMasurare)
            {
                return BadRequest("Measurement ID mismatch");
            }

            // Verifică dacă măsurarea există în baza de date
            var existingMeasurement = await _myProjectDbContext.Measurements.AsNoTracking()
                                                    .FirstOrDefaultAsync(m => m.IdMasurare == id);
            if (existingMeasurement == null)
            {
                return NotFound("Measurement not found");
            }

            // Asociază utilizatorul existent dacă este necesar
            if (measurementToUpdate.UserID != existingMeasurement.UserID)
            {
                var user = await _myProjectDbContext.User.FindAsync(measurementToUpdate.UserID);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                measurementToUpdate.User = user;
            }
            else
            {
                measurementToUpdate.User = existingMeasurement.User;
            }

            _myProjectDbContext.Entry(measurementToUpdate).State = EntityState.Modified;
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/measurements/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var measurement = await _myProjectDbContext.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound("Measurement not found");
            }

            _myProjectDbContext.Measurements.Remove(measurement);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
