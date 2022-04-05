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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Seat.Include(s => s.Play);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SeatingChart 
        // Method to access the seating chart by user. 
        public async Task<IActionResult> SeatingChart()
        {

            //var play = await _context.Play
                //.FirstOrDefaultAsync(m => m.PlayId == id);
            //List<Seat> seats = (List<Seat>)play.Seats;
            return View();
        }

        
        // GET: Seats/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
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
        public async Task<IActionResult> Edit(int id, [Bind("SeatId,SeatNumber,Price,PlayId")] Seat seat)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayId"] = new SelectList(_context.Play, "PlayId", "PlayName", seat.PlayId);
            return View(seat);
        }

        // GET: Seats/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seat = await _context.Seat
                .Include(s => s.Play)
                .FirstOrDefaultAsync(m => m.SeatId == id);
            if (seat == null)
            {
                return NotFound();
            }

            return View(seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seat = await _context.Seat.FindAsync(id);
            _context.Seat.Remove(seat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatExists(int id)
        {
            return _context.Seat.Any(e => e.SeatId == id);
        }
    }
}
