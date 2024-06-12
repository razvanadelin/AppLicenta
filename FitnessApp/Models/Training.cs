using FitnessApp.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Training
{
    [Key]
    public int ProgramID { get; set; }
    public DateTime DataIncepere { get; set; }
    public DateTime DataSf { get; set; }
    public int UserID { get; set; }
    public string NivelFitness { get; set; }
    public string ScopAntrenament { get; set; }

    [JsonIgnore]
    public User User { get; set; }
    public List<ExerciseTraining> ExerciseTraining { get; set; } = new List<ExerciseTraining>();
}
