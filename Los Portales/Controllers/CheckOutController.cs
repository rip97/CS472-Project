using Los_Portales.Data;
using Los_Portales.Models;
using Los_Portales.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Los_Portales.Controllers
{   
    [Authorize(Roles = "customer")]
    public class CheckOutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CheckOutController(ApplicationDbContext context, UserManager<IdentityUser> userManger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOutSuccess([Bind("CreditCardNumber,ExpirationDate,SecurityCode,NameOnCard")] Checkout checkout)
        {   if(ModelState.IsValid)
            {

                // get the shopping cart data
                var shoppingCart = getShoppingCart();
                
                // mark the sold seats in the data base 
                foreach(var item in shoppingCart.CartItems)
                {
                    item.seat.IsSold = 1;
                    if (!await updateSeat(item.SeatId, item.seat))
                        return NotFound();

                }

                // cretae the transaction and save to database 
                Transaction transaction = new Transaction();
                await createTransaction(transaction,shoppingCart);

                // dump cart objects in the database with asscoiated user Id 
                ViewBag.ShoppingCart = shoppingCart.CartItems;
                foreach (var item in shoppingCart.CartItems)
                {
                    if(!await deleteCartObject(item.Id, item))
                        return NotFound();
                }
               
                return View(transaction);
            }
            return Redirect(nameof(Checkout));
        }

        private ShoppingCartViewModel getShoppingCart()
        {
            var shoopingCart = new ShoppingCartViewModel();
            shoopingCart.CartItems = _context.Cart.ToList();
            var price = 0.0;
            foreach (var shoopingCartItem in shoopingCart.CartItems)
            {
                if (shoopingCartItem.UserId == GetUserId())
                {
                    getSeats(shoopingCartItem);
                    price += shoopingCartItem.seat.Price;
                }
            }
            shoopingCart.CartTotal = ((decimal)price);

            return shoopingCart;
        }

        /// <summary>
        /// Helpermethod to delete the cart objects 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cartObj"></param>
        /// <returns></returns>
        private async Task<bool> deleteCartObject(int id, Cart cartObj)
        {
            if (id != cartObj.Id)
                return false;
            else
            {
                var cart = await _context.Cart.FindAsync(id);
                _context.Cart.Remove(cartObj);
                await _context.SaveChangesAsync();
                return true;
            }

        }

        /// <summary>
        /// helper method to update the seat 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="seat"></param>
        /// <returns></returns>
        
        private async Task<bool> updateSeat(int id, Seat seat)
        {
            if (id != seat.SeatId)
            {
                return false;
            }
            else
            {
                _context.Update(seat);
                await _context.SaveChangesAsync();
                return true; 
            }


        }
        /// <summary>
        /// helper method to find the current user id
        /// </summary>
        /// <returns>string user id</returns>
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
            foreach (var seat in seats)
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
            foreach (var play in plays)
            {
                if (play.PlayId == seat.PlayId)
                {
                    seat.Play = play;
                    break;
                }
            }
        }

        /// <summary>
        /// helper method to create a transaction 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="checkout"></param>
        /// <returns></returns>
        private async Task createTransaction(Transaction transaction, ShoppingCartViewModel shoppingCart)
        {
            transaction.NumberOfSeats = shoppingCart.CartItems.Count;
            transaction.TotalCost = ((double)shoppingCart.CartTotal);
            int tranId = await getLastTransactionId() + 1000;
            transaction.TransactionId = tranId;
            await addTransaction(transaction);
            
        }

        /// <summary>
        /// Helper method to save a transaction to the database
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private async Task addTransaction(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            await _context.SaveChangesAsync();
        }

        private async Task<int> getLastTransactionId()
        {
            List<Transaction> transactions = _context.Transaction.ToList();
            if (transactions.Count == 0)
                return 1;
            else 
                return transactions.Count;
        }
    }
}
