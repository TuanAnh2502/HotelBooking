using HotelBooking.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelBooking.Controllers
{
    public class HomeController : Controller

    {

        private readonly HotelBookingContext _context;

        public HomeController(HotelBookingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            if (TempData.TryGetValue("UserId", out var userId) && TempData.TryGetValue("UserName", out var userName))
            {
                ViewBag.UserId = userId;
                ViewBag.UserName = userName;
            }
            ViewBag.IsLoggedIn = User.Identity.IsAuthenticated;
            return View(await _context.TblKhachSans.ToListAsync());
        }
        // GET: TblKhachSans/Details/5
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            // Hủy bỏ các thông tin đăng nhập của người dùng
            await HttpContext.SignOutAsync();

            // Điều hướng về trang chủ hoặc trang đăng nhập
            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKhachSan = await _context.TblKhachSans
                .FirstOrDefaultAsync(m => m.IdKhachsan == id);
            if (tblKhachSan == null)
            {
                return NotFound();
            }

            return View(tblKhachSan);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
