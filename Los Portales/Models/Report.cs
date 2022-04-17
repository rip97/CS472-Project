using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Report
    {   

        [Required]
        [DataType(DataType.Text)]
        public string PlayName { get; set; }

        public bool SeatsSold { get; set; } 

        public bool SeatsAvailabe { get; set; } 
        
        public bool TotalReveanue { get; set; } 

    }
}
