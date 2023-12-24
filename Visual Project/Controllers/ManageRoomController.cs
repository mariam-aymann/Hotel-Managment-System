using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;
using System.Runtime.Intrinsics.Arm;
using Visual_Project.Models;

namespace Visual_Project.Controllers
{
    public class ManageRoomController : Controller
    {
        private readonly HotelDbContext DbContext;
        public ManageRoomController(HotelDbContext context)
        {
            DbContext = context;
        }
        //   HotelDbContext DbContext = new HotelDbContext();

        // Add Room Controllers 
        [HttpGet]
        public IActionResult AddRoom()
        {
            return View("AddRoom");
        }
        [HttpPost]
        public IActionResult AddRoom(RoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                //RoomViewModel.AddRoom(room);
                var roomclassid = (from roomclass in DbContext.RoomClasses
                                   where roomclass.Type == room.RoomType
                                   select roomclass.ID).First();

                Room RoomToAdd = new()
                {
                    ID = room.RoomID,
                    Floor = room.Floor,
                    Price = room.Price,
                    Rate = room.Rate,
                    RoomClassID = roomclassid,
                    View = room.View,
                };
                DbContext.Rooms.Add(RoomToAdd);
                DbContext.SaveChanges();
                return RedirectToAction("ManageDetails", "Home");
            }
            return View();
        }

        // Remove Room Controllers
        [HttpGet]
        public IActionResult RemoveRoom()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RemoveRoom(string id)
        {
            if (ModelState.IsValid)
            {

                // Fetch the admin from the database based on the provided identifier
                var roomToRemove = DbContext.Rooms.FirstOrDefault(room => room.ID == id);

                if (roomToRemove != null)
                {
                    // Remove the admin
                    DbContext.Rooms.Remove(roomToRemove);
                    DbContext.SaveChanges();
                }
                // Optionally, you can redirect or return a success message
                return RedirectToAction("ManageDetails", "Home"); // Redirect to the admin list, adjust as needed
            }
            return View();

        }

        // Edit Room Controllers
        [HttpGet]
        public IActionResult EditRoom()
        {

            return View();
        }

        [HttpPost]
        public IActionResult EditRoom(RoomViewModel roomView)
        {
            if (ModelState.IsValid)
            {

                var room = DbContext.Rooms.Find(roomView.RoomID);
                var classRoom = (from roomclass in DbContext.RoomClasses
                                 where roomclass.Type == roomView.RoomType
                                 select roomclass).First();

                if (classRoom != null)
                    room.RoomClassID = classRoom.ID;
                if (room.Price != 0)
                    room.Price = roomView.Price;
                if (room.Rate != 0)
                    room.Rate = roomView.Rate;

                DbContext.SaveChanges();
                return RedirectToAction("ManageDetails", "Home");
            }
            return View();
        }
    }
}

