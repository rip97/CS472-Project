using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Cart
    {   
        [Key]
        public int Id { get; set; }

        public int SeatId { get; set; }

        public int PlayId { get; set; }

        public int UserId { get; set; }

        public virtual Seat? seat { get; set; }
     
    }

}
