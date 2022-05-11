using Xunit;
using Los_Portales.Controllers;
using Los_Portales.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Los_Portales.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System;


/* Code Written By: Justyn Rippie
 * Email: Justyn.Rippie@enmu.edu
 * File: PlaysControllerTest.cs
 */

// Note: I model this unit test after reading this article here on unit testing an asp.net-core application
// https://www.c-sharpcorner.com/article/getting-started-with-unit-testing-using-c-sharp-and-xunit/


namespace Los_Portales.Tests
{

    public class PlaysControllerTest
    {

        private DbContextOptions<ApplicationDbContext> dbContextOptions;

        private readonly ApplicationDbContext _context;

        private readonly string connStr = "Server=(localdb)\\mssqllocaldb;Database=aspnet-Los_Portales-CC6ED27B-4A3D-4DF9-83E6-73D5729066F6;Trusted_Connection=True;MultipleActiveResultSets=true";


        public PlaysControllerTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connStr);
            dbContextOptions = optionsBuilder.Options;
            _context = new ApplicationDbContext(dbContextOptions);

            DummyDataDBInit dummyData = new DummyDataDBInit();
            dummyData.Seed(_context);
            dummyData.SeedSeats(_context);
        }


        [Fact]
        public async void Index_GetAllPlays_ReturnsViewResult()
        {
            // Arrange      
            PlaysController plays = new PlaysController(_context);

            // Act 
            var result = await plays.Index();

            // Assert 
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Create_CreateANewPlay_ReturnsRedirectToActionresult()
        {
            //Arrange 
            Play play = new Play();
            play.PlayName = "Shrek";
            play.PlayDate = new DateTime(2022, 7, 10);
            play.PlayTime = new DateTime(2022, 7, 10, 13, 0, 0);


            PlaysController plays = new PlaysController(_context);


            // Act 
            var result = await plays.Create(play);

            //Assert 
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void Edit_EditsAPlay_ReturnsRedirectToActionResult()
        {
            //Arrange   
            
            PlaysController playsController = new PlaysController(_context);
            int id = 2;
            var play = await _context.FindAsync<Play>(2);
            play.PlayName = "Hamliton";

            // Act
            var result = await playsController.Edit(id, play);

            // Assert 
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]

        public async void DeleteConfirmed_DeletesAPlayWithIdNumber_ReturnsRedirectToActionResult()
        {
            //Arrange   

            PlaysController playsController = new PlaysController(_context);
            int id = 3;

            // Act
            var result = await playsController.DeleteConfirmed(id);

            // Assert 
            Assert.IsType<RedirectToActionResult>(result);
        }
        

       

    }  
}