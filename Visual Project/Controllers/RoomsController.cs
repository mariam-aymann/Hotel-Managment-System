﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Runtime.Intrinsics.Arm;
using Visual_Project.Models;

namespace Visual_Project.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HotelDbContext dbContext;
        public RoomsController(HotelDbContext context)
        {
            dbContext = context;
        }
       // HotelDbContext dbContext =new(); 
        public IActionResult Search()
        {
            return View();
        }
        public IActionResult Results()
        {
            return View();
        }
        [HttpGet]
        public IActionResult BookingPeriod()
        {
            return View();
        }

        public IActionResult BookingPeriod(RoomViewModel room)
        {
            HttpContext.Session.SetString("CheckInDate", room.Check_inDate);
            HttpContext.Session.SetString("CheckOutDate", room.Check_OutDate);
            return RedirectToAction("Search");
        }
        [HttpPost]
		public IActionResult Search(RoomViewModel room)
            
		{
  //          if (ModelState.IsValid)
  //          {
                room.Check_inDate = HttpContext.Session.GetString("CheckInDate");
                room.Check_OutDate = HttpContext.Session.GetString("CheckOutDate");

                List<RoomViewModel> Results = new();
                // Console.WriteLine(room.Room.Price);
                //Results = RoomViewModel.Result(room);
                DateTime From = DateTime.Parse(room.Check_inDate), To = DateTime.Parse(room.Check_OutDate);
                // Console.WriteLine(from);
                var roomclassid = (from roomclass in dbContext.RoomClasses
                                   where roomclass.Type == room.RoomType
                                   select roomclass.ID).First();


                var availableRooms = dbContext.Rooms
               .Include(r => r.Reservations)
               .Where(room => room.Reservations.All(reservation =>
               reservation.CheckOutDate <= From
               || reservation.CheckInDate >= To)
               && room.RoomClassID == roomclassid)
               .ToList();
                //  foreach (var room in availableRooms) Console.WriteLine(room.Reservations.First().Check_outDate);

                var rooms = (from rom in availableRooms
                             join roomclass in dbContext.RoomClasses
                             on rom.RoomClassID equals roomclass.ID
                             where rom.RoomClassID == roomclassid
                             && rom.Price <= room.Price
                             && rom.Rate == room.Rate
                             && rom.View == room.View
                             select new RoomViewModel
                             {
                                 RoomID = rom.ID,
                                 Price = rom.Price,
                                 Rate = rom.Rate,
                                 View = rom.View,
                                 RoomType = roomclass.Type,
                                 NumOfBeds = roomclass.NumOfBeds,
                                 Description = roomclass.Description,

                             }).ToList();


                Results = rooms;
                // return rooms;
                return View("Results", Results);
            //}
            //return View();

		}
		public IActionResult AddToCart(string id)

		{
            //if (ModelState.IsValid)
            //{

                // Retrieve the user's cart based on user authentication (you may use a more secure approach)
                var username = HttpContext.Session.GetString("Username");

                var user = dbContext.Guests.FirstOrDefault(u => u.Username == username);
                var CartItem = (from room in dbContext.Rooms
                                where room.ID == id
                                select room).First();

                var cart = HttpContext.Session.Get<List<Room>>("ShoppingCart") ?? new List<Room>();
                cart.Add(CartItem);
                HttpContext.Session.Set("ShoppingCart", cart);

                return RedirectToAction("Search", "Rooms");
            //}
            //return View();
		}

	}
}
