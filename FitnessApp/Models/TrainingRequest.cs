using FitnessApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace FitnessApp.Models
{
    public class TrainingRequest
    {
        public int UserID { get; set; }
        public string NivelFitness { get; set; }
        public string ScopAntrenament { get; set; }
    }
}
