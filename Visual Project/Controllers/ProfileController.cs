using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Visual_Project.Models;

namespace Visual_Project.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HotelDbContext dbcontext;
        public ProfileController(HotelDbContext context)
        {
            dbcontext = context;
        }
      
        public IActionResult Profile()
        {

            var username = HttpContext.Session.GetString("Username");
           

            var guest = (from user in dbcontext.Guests
                         where user.Username == username
                         select user).FirstOrDefault();

            var admin = (from user in dbcontext.Admins
                         where user.Username == username
                         select user).FirstOrDefault();

           // Console.WriteLine(guest.Username);
            if (guest != null) ViewBag.user = guest;
            else ViewBag.user = admin;
            return View();
        }

        public IActionResult EditProfile()
        {
            var username = HttpContext.Session.GetString("Username");
            var guest = (from user in dbcontext.Guests
                         where user.Username == username
                         select user).FirstOrDefault();

            var admin = (from user in dbcontext.Admins
                         where user.Username == username
                         select user).FirstOrDefault();

            if (guest != null) ViewBag.user = guest;
            else ViewBag.user = admin;
            return View();
        }
        [HttpPost]
        public IActionResult EditProfileDetails(Guest updateGuest)
        {
            var username = HttpContext.Session.GetString("Username");
            var guest = (from user in dbcontext.Guests
                         where username == user.Username
                         select user).FirstOrDefault();
           
            
            if (updateGuest.Firstname != null)
                guest.Firstname = updateGuest.Firstname;
            if (updateGuest.Lastname != null)
                guest.Lastname = updateGuest.Lastname;
            if (updateGuest.Email != null)
                guest.Email = updateGuest.Email;
            if (updateGuest.PhoneNumber != null)
                guest.PhoneNumber = updateGuest.PhoneNumber;
            if (updateGuest.Password != null)
                guest.Password = updateGuest.Password;
            dbcontext.SaveChanges();
            return RedirectToAction("Profile");
        }
        public IActionResult Logout()
        {
            // Clear the user's authentication status
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect to the homepage after logout
        }

    }
}
