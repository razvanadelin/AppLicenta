using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var measurements = await _myProjectDbContext.Measurements.ToListAsync();

            if (measurements.Count == 0)
            {
                return NotFound("Measurements not found");
            }

            return Ok(measurements);
        }
    }
}
