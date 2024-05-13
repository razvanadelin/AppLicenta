using FitnessApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {
        private readonly ProjectDbContext _myProjectDbContext;
        public MealPlanController(ProjectDbContext myProjectDbContext)
        {
            _myProjectDbContext = myProjectDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {

            var MealPlans = await _myProjectDbContext.MealPlan.ToListAsync();

            if (MealPlans.Count == 0)
            {
                return NotFound("MealPlans not found");
            }

            return Ok(MealPlans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEquipmentByIdAsync(int id)
        {
            var actualTask = await _myProjectDbContext.MealPlan.AsNoTracking().Where(ats => ats.PlanID == id).FirstOrDefaultAsync();

            if (actualTask == null)
            {
                return NotFound("User not found");
            }

            return Ok(actualTask);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(MealPlan MealPlan)
        {
            if (MealPlan == null)
            {
                return BadRequest("Request is incorrect");
            }

            if (MealPlan.UserID <= 0)
            {
                return BadRequest("UserID is required");
            }

            var user = await _myProjectDbContext.User.FindAsync(MealPlan.UserID);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.MealPlans.Add(MealPlan);
            await _myProjectDbContext.SaveChangesAsync();
            return Created($"/id?id={MealPlan.PlanID}", MealPlan);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, MealPlan actualTaskToUpdate)
        {
            var actualTask = await _myProjectDbContext.MealPlan.AsNoTracking().Where(ats => ats.PlanID == id).FirstOrDefaultAsync();

            if (actualTask == null)
            {
                return NotFound();
            }
            _myProjectDbContext.MealPlan.Update(actualTaskToUpdate);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var actualTask = await _myProjectDbContext.MealPlan.FindAsync(id);
            if (actualTask == null)
            {
                return NotFound();
            }
            _myProjectDbContext.MealPlan.Remove(actualTask);
            await _myProjectDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}

