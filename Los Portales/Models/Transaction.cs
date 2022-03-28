namespace Los_Portales.Models
{
    public class Transaction
    {   
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public string SelectedSeats { get; set; }
        public int NumberOfTickets { get; set; }
        public long PaymentMethod { get; set; }


        public decimal GetTax() { return Tax; }
        public int GetNumberOfTickets() { return NumberOfTickets;  }
        public decimal CalcTotal() { return Total = NumberOfTickets * Tax; }
        public Boolean UpdatePaymentMethod() { return PaymentMethod != 0; }
        private static void UpdateTransactionTable() { }
    }
}
