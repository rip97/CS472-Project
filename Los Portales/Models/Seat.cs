using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Los_Portales.Models
{
    public class Seat
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }
        [Required]
        public int SeatNumber { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }

        // 0 denotes false, 1 denotes true
        public int IsSold { get; set; }

        // to show that the seat belongs to a certain play 
        public int PlayId { get; set; }
        public Play? Play { get; set; }


    }
}
