using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Play
    {
        public int PlayId { get; set; }
        [Required]
        public string PlayName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PlayDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime PlayTime { get; set; }

        // a play can have many seats 
        public ICollection<Seat>? Seats { get; set; }
        
    }
}
