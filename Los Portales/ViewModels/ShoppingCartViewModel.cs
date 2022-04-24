using System.Collections.Generic;
using Los_Portales.Models;
using System.ComponentModel.DataAnnotations;

namespace Los_Portales.ViewModels
{
    public class ShoppingCartViewModel
    {   
        public List<Cart>? CartItems { get; set; }

        [DataType(DataType.Currency)]
        public decimal CartTotal { get; set; }
    }
}
