using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int NumberOfSeats { get; set; }
        public double TotalCost { get; set; }
           
    }
}
