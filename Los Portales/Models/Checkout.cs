﻿using System.ComponentModel.DataAnnotations;

namespace Los_Portales.Models
{
    public class Checkout
    {   
        [Required]
        [DataType(DataType.CreditCard)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0000 0000 0000 0000}")]
        public string CreditCardNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Year { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:000}")]
        public string SecurityCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string? NameOnCard { get; set; } 
        
    }
}
