using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HotelBooking.Controllers
{
    public class QuanlyphongController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            return View();
        }
    }
}
