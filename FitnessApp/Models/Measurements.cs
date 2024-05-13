using System.ComponentModel.DataAnnotations;

namespace FitnessApp.Models
{
    public class Measurements
    {
        [Key]
        public int IdMasurare { get; set; }   
        public DateTime Data { get; set; }
        
        public decimal Greutate { get; set; }
        public decimal Inaltime { get; set; }
        public decimal CircTalie { get; set; }
        public decimal CircSold { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }
    }
}
