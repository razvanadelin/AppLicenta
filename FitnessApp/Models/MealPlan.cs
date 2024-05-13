using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MealPlan
    {
        [Key]
        public int PlanID { get; set; }
        public DateTime DataPlan { get; set; }
        public DateTime DataSfPlan { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
    }
}
