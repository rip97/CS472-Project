using Los_Portales.Models;
using Los_Portales.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Los_Portales.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;



namespace Los_Portales.Controllers
{   
    [Authorize]
    public class ShoppingCartController : Controller
    {   
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ShoppingCartController(ApplicationDbContext context, UserManager<IdentityUser> userManger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IActionResult> Index()
        {
            var shoopingCart = new ShoppingCartViewModel();
            shoopingCart.CartItems = await _context.Cart.ToListAsync();
            var price = 0.0;

            
            foreach (var shoopingCartItem in shoopingCart.CartItems)
            {   
                if(shoopingCartItem.UserId == GetUserId())
                {
                    getSeats(shoopingCartItem);
                    price += shoopingCartItem.seat.Price;
                }
            }
            shoopingCart.CartTotal = ((decimal)price);
            return View(shoopingCart); 
        }


        /// <summary>
        /// Helper Method to obtain the current user ID 
        /// </summary>
        /// <returns></returns>
        private string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return _userManager.GetUserId(user);
        }

        // write a binary search for here for better preformace 
        // helper method to search for correct seats 
        private void getSeats(Cart cart)
        {
            var seats = _context.Seat.ToList();
            foreach(var seat in seats)
            {
                if (cart.SeatId == seat.SeatId)
                {
                    cart.seat = seat;
                    getPlay(seat);
                    break;
                }
            }
        }
        
        /// <summary>
        /// helper method to find play 
        /// </summary>
        /// <param name="seat"></param>
        private void getPlay(Seat seat)
        {
            var plays = _context.Play.ToList();
            foreach(var play in plays)
            {
                if(play.PlayId == seat.PlayId)
                {
                    seat.Play = play;
                    break; 
                }             
            }
        }
    
        /// <summary>
        /// Method Adds the seat to the cart object and stores it in the database 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int? id)
        {   
            if(id == null)
            {
                return NotFound();
            }
            var seat = await _context.Seat
                .FirstOrDefaultAsync(m => m.SeatId == id);
            var play = await _context.Play.FirstOrDefaultAsync(m => m.PlayId == seat.PlayId);
            seat.Play = play;
            await addItem(seat);
            return RedirectToAction(nameof(Index)); 
        }

        /// <summary>
        /// Helper method to create cart object and add to database 
        /// </summary>
        /// <param name="seat"></param>
        /// <returns></returns>
       
        private async Task addItem(Seat seat)
        {
            
            Cart cartObj = new();
            cartObj.SeatId = seat.SeatId;
            cartObj.PlayId = seat.PlayId;
            cartObj.seat = seat;
            cartObj.UserId = GetUserId();
            _context.Cart.Add(cartObj);
            await _context.SaveChangesAsync();
           
        }

        /// <summary>
        /// Remove Item from cart 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
