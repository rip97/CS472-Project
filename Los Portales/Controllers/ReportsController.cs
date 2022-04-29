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

        // POST: Reports/Index
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
                
                return View(report);
            }

            return RedirectToAction("Index");
        }                 
            
    }   

}
