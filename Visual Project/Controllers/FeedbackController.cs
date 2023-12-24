using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Visual_Project.Models;

namespace Visual_Project.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly HotelDbContext DbContext;
        public FeedbackController(HotelDbContext context)
        {
            DbContext = context;
        }

        [HttpGet]
        public IActionResult Feedback()
        {


            
            return View();
        }
        [HttpPost]
        public IActionResult AddFeedBack(FeedBack feedback)
        {
            if (ModelState.IsValid)
            {
                DbContext.FeedBacks.Add(feedback);
                DbContext.SaveChanges();
                //  FeedBack.AddFeedBack(feedback);
                return View();
            }
            return View();
        }


        [HttpGet]
        public IActionResult Testimonial()
        {
            List<FeedBack> list = new List<FeedBack>();
            //  list = FeedBack.GetFeedBacks();
             list  = DbContext.FeedBacks
          .Include(r => r.Reservation)
          .ThenInclude(t => t.Guest)
           .ToList();
       
            return View("Testimonial", list);
           

        }
    }
}
