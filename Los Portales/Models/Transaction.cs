using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public string SelectedSeats { get; set; }
        public int NumberOfTickets { get; set; }
        [DataType(DataType.CreditCard)]
        public long PaymentMethod { get; set; }


    }
}
