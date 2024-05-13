using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Exercices
    {
        [Key]
        public int ExID { get; set; }
        public string NumeEx { get; set; }
        public string Descriere {  get; set; }
        public string Categorie { get; set; }

        public List<ExerciseTraining> ExerciseTraining = new List<ExerciseTraining>();
    }
}
