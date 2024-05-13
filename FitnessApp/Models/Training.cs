using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Training
    {
        [Key]
        public int ProgramID { get; set; }
        public DateTime DataIncepere { get; set; }
        public DateTime DataSf { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
       

        public List<ExerciseTraining> ExerciseTraining = new List<ExerciseTraining>();

    }
}
