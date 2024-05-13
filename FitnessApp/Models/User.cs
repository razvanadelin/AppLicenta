using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FitnessApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string ContactNr { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }

        public List<MealPlan> MealPlans = new List<MealPlan>();
        
        public List<Training> Trainings = new List<Training>();

        public List<Measurements> Measurements = new List<Measurements>();
        public List<Achievements> Achievements = new List<Achievements>();
    }
}
