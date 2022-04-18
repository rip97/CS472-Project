using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public ICollection<Seat>? Seats { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal CalcTotal() { return Total = NumberOfTickets * Tax; }

        public string PaymentMethod
        {         
            get
            {
                return "x" + CreditCardNumber.ToString().Substring(12, 4) + " " +
                        ExpirationDate.ToString("MM/yyyy");
            }
        }
        [Required]
        [DataType(DataType.CreditCard)]
        public long CreditCardNumber { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public int SecurityCode { get; set; }
        [Required]
        public string? NameOnCard { get; set; }
    }
}
