using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class MealPlan
    {
        [Key]
        public int PlanID { get; set; }
        public DateTime DataPlan { get; set; }
        public int NrCalorii { get; set; }
        public string? DescrierePlan { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
