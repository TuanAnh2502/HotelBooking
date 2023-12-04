using Azure.Core;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class ChitietphongController : Controller
    {
        private readonly ILogger<ChitietphongController> _logger;
        private readonly HotelBookingContext _context;
        public ChitietphongController(ILogger<ChitietphongController> logger, HotelBookingContext context)
        {
            _logger = logger;
            _context = context;
        }
/*        [Route("/Chitietphong")]
*/        public async Task<IActionResult> Index(int? idkhachsan)
        {
            string kieuPhong = HttpContext.Request.Query["kieuphong"];

            // Gán giá trị vào ViewBag để sử dụng trong view
            ViewBag.kieu = kieuPhong;
            var hotelBookingContext = _context.TblPhongs.Include(t => t.IdKhachsanNavigation).Where(t=> t.IdKhachsan==6);
            return View(await hotelBookingContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPhong = await _context.TblPhongs
                .Include(t => t.IdKhachsanNavigation)
                .FirstOrDefaultAsync(m => m.IdPhong == id);
            if (tblPhong == null)
            {
                return NotFound();
            }

            return View(tblPhong);
        }

    }
}