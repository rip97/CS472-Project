#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Los_Portales.Data;
using Los_Portales.Models;
using Microsoft.AspNetCore.Authorization;

namespace Los_Portales.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        // GET: Seats 
        // seat 
        public async Task<IActionResult> Index(int id)
        {
            var applicationDbContext = _context.Seat.Include(s => s.Play);
            return View(findCorrectSeats(id, await applicationDbContext.ToListAsync()));
        }

        /// <summary>
        /// Will route a seat to the shopping cart 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        // GET: Id is the seat Id
        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var seat = await _context.Seat
                .FirstOrDefaultAsync(m => m.SeatId == id);
            var play = await _context.Play.FirstOrDefaultAsync(m => m.PlayId == seat.PlayId);
            seat.Play = play;
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        [Authorize]
        // GET: SeatingChart 
        // Method to access the seating chart by user. 
        public async Task<IActionResult> SeatingChart(int id)
        {
            var applicationDbContext = _context.Seat.Include(s => s.Play);
            List<Seat> seats = findCorrectSeats(id, await applicationDbContext.ToListAsync());
            List<int> isInCart = new List<int>();
            foreach(var item in _context.Cart.ToList())
            {
                if (item.PlayId == id)
                    isInCart.Add(item.SeatId);
            }
            ViewBag.IsInCart = isInCart;
            return View(seats);
        }

        /// <summary>
        /// Method returns a list of correct seats with a single play id 
        /// </summary>
        /// <param name="searchVal"></param>
        /// <param name="seats"></param>
        /// <returns> List of seats </returns>
        private List<Seat> findCorrectSeats(int searchVal, List<Seat> seats)
        {   
            List<Seat> result = new List<Seat>();
            foreach (var seat in seats)
            {
                if(seat.PlayId == searchVal)
                {
                    result.Add(seat);
                }
            }
            return result;
        }
    
        // GET: Seats/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ((id) == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat.FindAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            ViewData["PlayId"] = new SelectList(_context.Play, "PlayId", "PlayName", seat.PlayId);
            return View(seat);
        }

        // POST: Seats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id,[Bind("SeatId,SeatNumber,Price,PlayId")] Seat seat)
        {
            if (id != seat.SeatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatExists(seat.SeatId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Seats", new {id= seat.PlayId});
            }
            ViewData["PlayId"] = new SelectList(_context.Play, "PlayId", "PlayName", seat.PlayId);
            return View(seat);
        }

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.SeatId == id);
        }
    }

}
