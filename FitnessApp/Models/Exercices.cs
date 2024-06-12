using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitnessApp.Models
{
   
        public class Exercices
        {
            [Key]
            public int ExID { get; set; }
            public string NumeEx { get; set; }
            public string Descriere { get; set; }
            public string Categorie { get; set; }

            [JsonIgnore]
            public List<ExerciseTraining> ExerciseTraining { get; set; } = new List<ExerciseTraining>();
        }
    }
