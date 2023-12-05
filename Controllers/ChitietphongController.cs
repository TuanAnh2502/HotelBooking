using Azure.Core;
using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

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
        */
        public async Task<IActionResult> Index(int? id, string tiennghi,string kieuphong)
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            /*
                        ViewBag.idks = id;
                        ViewBag.mota = (await _context.TblKhachSans.FirstOrDefaultAsync(m => m.IdKhachsan == id))?.SMotakhachsan;
                        ViewBag.tenks = (await _context.TblKhachSans.FirstOrDefaultAsync(m => m.IdKhachsan == id))?.STenkhachsan;
                        tiennghi= HttpContext.Request.Form["tiennghi"];
                        kieuphong = HttpContext.Request.Form["kieuphong"];
                        ViewBag.tiennghi = tiennghi;
                        var query = _context.TblPhongs.Include(h => h.IdKhachsanNavigation).AsQueryable();

                        // Thêm điều kiện lọc cho đánh giá (rating)
                        if (id.HasValue)
                        {

                            query = query.Where(t => t.IdKhachsan==id);
                        }

                        if (!string.IsNullOrEmpty(tiennghi))
                        {

                            query = query.Where(t=> t.STiennghi.ToLower().Contains(tiennghi));
                        }

                        // Thêm điều kiện lọc cho địa chỉ (address)
                        if (!string.IsNullOrEmpty(kieuphong))
                        {
                            query = query.Where(t=>t.SKieuPhong.ToLower().Contains(kieuphong));
                        }

                        var hotelsList = await query.ToListAsync();
                        return View(hotelsList);*/
            kieuphong = HttpContext.Request.Query["kieuphong"];
            tiennghi = HttpContext.Request.Query["tiennghi"];
            ViewBag.idks = id;
            // Gán giá trị vào ViewBag để sử dụng trong view
            ViewBag.kieu = kieuphong;
            ViewBag.tiennghi = tiennghi;
            ViewBag.mota = (await _context.TblKhachSans.FirstOrDefaultAsync(m => m.IdKhachsan == id))?.SMotakhachsan;
            ViewBag.tenks = (await _context.TblKhachSans.FirstOrDefaultAsync(m => m.IdKhachsan == id))?.STenkhachsan;
            var tblPhongs = await _context.TblPhongs.Include(t => t.IdKhachsanNavigation).Where(t => t.IdKhachsan == id).ToListAsync();
            if (string.IsNullOrEmpty(kieuphong) && string.IsNullOrEmpty(tiennghi))
            {
                return View(tblPhongs);
            }
            else
            {
                var tblKieu = await _context.TblPhongs.Include(t => t.IdKhachsanNavigation)
                                        .Where(t => t.IdKhachsan == id && (string.IsNullOrEmpty(kieuphong) || t.SKieuPhong == kieuphong) && (string.IsNullOrEmpty(tiennghi) || t.STiennghi.ToLower().Contains(tiennghi))).ToListAsync();

                return View(tblKieu);
            }
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