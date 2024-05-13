namespace FitnessApp.Models
{
    public class Achievements
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int RealizareId { get; set; }

        public string TipRealizare { get; set; }
        public DateTime DataRealizare { get; set; }
        public int UserID { get; set; }

        public User User { get; set; }
    }
}
