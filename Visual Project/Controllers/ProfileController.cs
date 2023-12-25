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
           

            var CurUser = (from user in dbcontext.Users
                         where user.Username == username
                         select user).FirstOrDefault();

           
            return View(CurUser);
        }

        public IActionResult EditProfile()
        {
            var username = HttpContext.Session.GetString("Username");


            var CurUser = (from user in dbcontext.Users
                           where user.Username == username
                           select user).FirstOrDefault();


            return View(CurUser);
        }
        [HttpPost]
        public IActionResult EditProfileDetails(EndUser updateUser)
        {
            var username = HttpContext.Session.GetString("Username");
    
            var CurUser = (from user in dbcontext.Users
                           where user.Username == username
                           select user).FirstOrDefault();


           
                if (updateUser.Firstname != null)
                    CurUser.Firstname = updateUser.Firstname;
                if (updateUser.Lastname != null)
                    CurUser.Lastname = updateUser.Lastname;
                if (updateUser.Email != null)
                    CurUser.Email = updateUser.Email;
                if (updateUser.PhoneNumber != null)
                    CurUser.PhoneNumber = updateUser.PhoneNumber;
            if (updateUser.Password != null)
            {
                CurUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(updateUser.Password, 13);
              //  CurUser.Password = updateUser.Password;
            }
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
