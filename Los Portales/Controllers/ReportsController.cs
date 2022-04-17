using Los_Portales.Data;
using Los_Portales.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Los_Portales.Controllers
{
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

       // GET: Reports/Index
        public IActionResult Index()
        {
            ViewBag.Plays = getPlays();
            return View();
        }

        // POST: Reports/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("PlayName,SeatsSolds,SeatsAvailabe,TotalReveanue")] Report report)
        {      
            if(ModelState.IsValid)
            {
                // do some stufr here for the report
                
                //if statements for each tpye of report
                // revaanue report
                if(report.TotalReveanue == true && report.SeatsSold != true && report.SeatsAvailabe != true)
                {

                }
                else if (report.TotalReveanue != true && report.SeatsSold == true && report.SeatsAvailabe != true) // seats sold report
                {

                }
                else if (report.TotalReveanue != true && report.SeatsSold != true && report.SeatsAvailabe == true) // seats available report 
                {

                }
                else if(report.TotalReveanue == true && report.SeatsSold == true) // seats sold with total reveanue 
                {

                }
                else if (report.TotalReveanue == true && report.SeatsAvailabe == true) // seats available with total reveanue
                {

                }
                else if(report.SeatsAvailabe == true && report.SeatsSold == true) // seats sold and available
                {

                }
                else // report everything 
                {

                }


                    return RedirectToAction(nameof(Report));
            }
            ViewBag.Plays = getPlays();
            return View(report);
        }

        public IActionResult Report()
        {
            return View();
        }
    }
}
