using Los_Portales.Data;
using Los_Portales.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Los_Portales.Controllers
{   
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        private List<Play> getPlays()
        {
            return _context.Play.ToList();
        }

        private List<Seat> findCorrectSeats(int searchVal, List<Seat> seats)
        {
            List<Seat> result = new List<Seat>();
            foreach (var seat in seats)
            {
                if (seat.PlayId == searchVal)
                {
                    result.Add(seat);
                }
            }
            return result;
        }


        // GET: Reports/Index
        public IActionResult Index()
        {
            ViewBag.Plays = getPlays();
            return View();
        }

        // POST: Reports/Report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Report([Bind("PlayName,SeatsSold,SeatsAvailabe")] Report report)
        {
            if (ModelState.IsValid)
            {
                if (report.SeatsSold == false && report.SeatsAvailabe == false)
                    return Redirect("Index");

                int playId = 0;
                foreach (var item in getPlays())
                {
                    if (report.PlayName == item.PlayName)
                    {
                        playId = item.PlayId;
                        break;
                    }
                }
                report.seats = findCorrectSeats(playId, _context.Seat.ToList());

                ViewBag.Total = getTotal(playId, _context.Seat.ToList());
                ViewBag.TotalSoldSeats = getTotalSoldSeats(playId, _context.Seat.ToList());
                ViewBag.TotalAvailableSeats = getTotalAvailableSeats(playId, _context.Seat.ToList());


                return View(report);
            }

            return RedirectToAction("Index");
        }


        private List<Seat> findSoldSeats(int searchVal, List<Seat> seats)
        {
            List<Seat> result = new List<Seat>();
            foreach (var seat in seats)
            {
                if (seat.PlayId == searchVal && seat.IsSold == 1)
                {
                    result.Add(seat);
                }
            }
            return result;
        }

        private float getTotal(int searchVal, List<Seat> seats)
        {
            List<Seat> result = findSoldSeats(searchVal, seats);

            float total = 0;
            foreach (var seat in result)
            {
                total = total + (float)seat.Price;
            }
            return total;
        }

        private float getTotalSoldSeats(int searchVal, List<Seat> seats)
        {
            List<Seat> result = findSoldSeats(searchVal, seats);

            float total = 0;
            foreach (var seat in result)
            {
                total = total + 1;
            }
            return total;
        }

        private List<Seat> findAvailableSeats(int searchVal, List<Seat> seats)
        {
            List<Seat> result = new List<Seat>();
            foreach (var seat in seats)
            {
                if (seat.PlayId == searchVal && seat.IsSold == 0)
                {
                    result.Add(seat);
                }
            }
            return result;
        }

        private float getTotalAvailableSeats(int searchVal, List<Seat> seats)
        {
            List<Seat> result = findAvailableSeats(searchVal, seats);

            float total = 0;
            foreach (var seat in result)
            {
                total = total + 1;
            }
            return total;
        }

    }   

}
