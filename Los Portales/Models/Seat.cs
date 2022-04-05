using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Seat
    {
        public int SeatId { get; set; }
        [Required]
        public int SeatNumber { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        // to show that the seat belongs to a certain play 
        public int PlayId { get; set; }
        public Play? Play { get; set; }

    }
}
