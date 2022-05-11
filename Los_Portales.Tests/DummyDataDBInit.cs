using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Los_Portales.Data;
using Los_Portales.Models;
using System;

/* Code Written By: Justyn Rippie
 * Email: Justyn.Rippie@enmu.edu
 * File: DummyDataDBInit.cs
 */

namespace Los_Portales.Tests
{
    public class DummyDataDBInit
    {
        public DummyDataDBInit()
        {

        }
        
        public void SeedSeats(ApplicationDbContext context)
        {
            foreach (var item in context.Play)
            {
                for (int i = 0; i < 80; i++)
                {
                    context.Seat.Add(
                        new Seat()
                        {
                            PlayId = item.PlayId,
                            SeatNumber = i + 1,
                            IsSold = 0,
                            Price = 25,
                            Play = item

                        }); ;
                    

                }

            }
            context.SaveChanges();
        }
        public void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Play.AddRange
            (
                new Play() { PlayName = "Coco", PlayDate = new DateTime(2022, 8, 8), PlayTime = new DateTime(2022, 8, 8, 13, 0, 0) },
                new Play() { PlayName = "Soul", PlayDate = new DateTime(2022, 6, 10), PlayTime = new DateTime(2022, 6, 10, 13, 0, 0) },
                new Play() { PlayName = "Soul", PlayDate = new DateTime(2022, 6, 10), PlayTime = new DateTime(2022, 6, 10, 19, 0, 0) }

            );

            context.SaveChanges();

            
        }


    }
}
