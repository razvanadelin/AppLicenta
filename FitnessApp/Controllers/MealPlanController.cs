using FitnessApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly ProjectDbContext _context;

        public MealPlanController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: api/MealPlan
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var mealPlans = await _context.MealPlan.Include(mp => mp.User).ToListAsync();

            if (mealPlans.Count == 0)
            {
                return NotFound("Meal plans not found");
            }

            return Ok(mealPlans);
        }

        // GET: api/MealPlan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMealPlanByIdAsync(int id)
        {
            var mealPlan = await _context.MealPlan.Include(mp => mp.User)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(mp => mp.PlanID == id);

            if (mealPlan == null)
            {
                return NotFound("Meal plan not found");
            }

            return Ok(mealPlan);
        }

        // GET: api/MealPlan/calories/{calories}
        [HttpGet("calories/{calories}")]
        public async Task<IActionResult> GetMealPlanByCalories(int calories)
        {
            var mealPlan = await _context.MealPlan
                .Where(mp => mp.NrCalorii == calories)
                .FirstOrDefaultAsync();

            if (mealPlan == null)
            {
                return NotFound("Meal plan not found");
            }

            return Ok(mealPlan);
        }

        // POST: api/MealPlan
        [HttpPost]
        public async Task<IActionResult> PostAsync(MealPlan mealPlan)
        {
            if (mealPlan == null)
            {
                return BadRequest("Request is incorrect");
            }

            if (mealPlan.UserID <= 0)
            {
                return BadRequest("UserID is required");
            }

            var user = await _context.User.FindAsync(mealPlan.UserID);
            if (user == null)
            {
                return NotFound("User not found");
            }

            mealPlan.User = user;
            _context.MealPlan.Add(mealPlan);
            await _context.SaveChangesAsync();
            return Created($"/api/MealPlan/{mealPlan.PlanID}", mealPlan);
        }

        // PUT: api/MealPlan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, MealPlan mealPlanToUpdate)
        {
            if (id != mealPlanToUpdate.PlanID)
            {
                return BadRequest("Meal Plan ID mismatch");
            }

            var existingMealPlan = await _context.MealPlan.AsNoTracking()
                                                    .FirstOrDefaultAsync(mp => mp.PlanID == id);
            if (existingMealPlan == null)
            {
                return NotFound("Meal plan not found");
            }

            if (mealPlanToUpdate.UserID != existingMealPlan.UserID)
            {
                var user = await _context.User.FindAsync(mealPlanToUpdate.UserID);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                mealPlanToUpdate.User = user;
            }
            else
            {
                mealPlanToUpdate.User = existingMealPlan.User;
            }

            _context.Entry(mealPlanToUpdate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/MealPlan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var mealPlan = await _context.MealPlan.FindAsync(id);
            if (mealPlan == null)
            {
                return NotFound("Meal plan not found");
            }

            _context.MealPlan.Remove(mealPlan);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // GET: api/MealPlan/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMealPlansByUserId(int userId)
        {
            var mealPlans = await _context.MealPlan
                                          .Where(mp => mp.UserID == userId)
                                          .ToListAsync();

            if (mealPlans == null || mealPlans.Count == 0)
            {
                return NotFound("Meal plans not found for the user");
            }

            return Ok(mealPlans);
        }

    }
}
