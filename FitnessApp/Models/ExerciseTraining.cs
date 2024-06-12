using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class ExerciseTraining
    {
        [Key]
        public int ExerciseTrainingID { get; set; }
        public int ExID { get; set; }
        public int ProgramID { get; set; }
        public Training Training { get; set; }
        public Exercices Exercices { get; set; }

    }
}

