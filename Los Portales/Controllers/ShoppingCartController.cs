using Los_Portales.Models;
using Los_Portales.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Los_Portales.Data;

namespace Los_Portales.Controllers
{   
    public class ShoppingCartController : Controller
    {   
        private readonly ApplicationDbContext _context;
        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        
        public IActionResult AddToCart()
        {
            return View(); 
        }

       
    }
}
