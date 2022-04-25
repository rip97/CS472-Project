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
    public class PlaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Plays
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Play.ToListAsync());
        }

        [Authorize(Roles ="admin")]
        // GET: Plays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PlayId,PlayName,PlayDate,PlayTime")] Play play)
        {
            if (ModelState.IsValid)
            {   
                
                _context.Add(play);
                await _context.SaveChangesAsync();
                var newPlay = await _context.Play
                 .FirstOrDefaultAsync(m => m.PlayId == play.PlayId);

                // below code creates 80 defualt seats for the new play, the admin can edit the seat prices from the seat controller for each play
                List<Seat> seats = CreateSeats(newPlay);
                foreach(Seat seat in seats)
                {
                    await Create(seat);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(play);
        }

        /// <summary>
        /// saves the new seat to database 
        /// </summary>
        /// <param name="seat"></param>
        /// <returns> none </returns>
        private async Task Create(Seat seat)
        {
            _context.Add(seat);
            await _context.SaveChangesAsync();           
        }

        [Authorize(Roles = "admin")]
        // GET: Plays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var play = await _context.Play.FindAsync(id);
            if (play == null)
            {
                return NotFound();
            }
            return View(play);
        }

        // POST: Plays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PlayId,PlayName,PlayDate,PlayTime")] Play play)
        {
            if (id != play.PlayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(play);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayExists(play.PlayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(play);
        }

        // GET: Plays/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var play = await _context.Play
                .FirstOrDefaultAsync(m => m.PlayId == id);
            if (play == null)
            {
                return NotFound();
            }

            return View(play);
        }

        // POST: Plays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var play = await _context.Play.FindAsync(id);
            _context.Play.Remove(play);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayExists(int id)
        {
            return _context.Play.Any(e => e.PlayId == id);
        }

        /// <summary>
        /// method generates 80 new seats for a new play with defualt values 
        /// </summary>
        /// <param name="play"></param>
        /// <returns> list of seats </returns>
        private List<Seat> CreateSeats(Play play)
        {   
            List<Seat> seats = new List<Seat>();
            for (int i = 0; i < 80; i++)
            {
                Seat seat = new Seat();
                seat.PlayId = play.PlayId;
                seat.Play = play;
                seats.Add(seat);
                seat.SeatNumber = i + 1;
                seat.Price = 0.00;
                
                
            }
            return seats;
        }
        
    }
}
