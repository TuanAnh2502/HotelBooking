using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;
using System.Text;

namespace HotelBooking.Controllers
{
    public class TblDatphongsController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblDatphongsController(HotelBookingContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index(decimal? rating, string address, DateTime? nhanphong, DateTime? traphong, int? number)
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            var query = _context.TblKhachSans.Include(h => h.IdUserNavigation).AsQueryable();
            //Thêm điều kiện lọc cho địa chỉ + đánh giá
            if (rating.HasValue&& !string.IsNullOrEmpty(address))
            {
                decimal lowerBound = rating ?? 0;
                decimal upperBound = rating.HasValue ? rating.Value + 1 : 0;
                var combinedData = new CombinedData { Address = address, Rating = (int)rating };
                query = query.Where(h => h.SDanhgia >= lowerBound && h.SDanhgia < upperBound && h.SDiachi.ToLower().Contains(address));
                return View(combinedData);
            }
            // Thêm điều kiện lọc cho đánh giá (rating)
            if (rating.HasValue)
            {
                decimal lowerBound = rating ?? 0;
                decimal upperBound = rating.HasValue ? rating.Value + 1 : 0;
                
                query = query.Where(h => h.SDanhgia >= lowerBound && h.SDanhgia < upperBound);

            }
            
            // Thêm điều kiện lọc cho địa chỉ (address)
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(khachsan => khachsan.SDiachi.ToLower().Contains(address));
            }

            // Thêm các điều kiện lọc khác tùy thuộc vào yêu cầu của bạn
            if (nhanphong.HasValue)
            {
                /*query = query.Where(h => *//* Thêm logic kiểm tra ngày nhận phòng *//*);*/
            }

            if (traphong.HasValue)
            {
                /*query = query.Where(h => *//* Thêm logic kiểm tra ngày trả phòng *//*);*/
            }

            if (number.HasValue)
            {
                /*query = query.Where(h => *//* Thêm logic kiểm tra số người *//*);*/
            }

            var hotelsList = await query.ToListAsync();
            return View(hotelsList);
            // Trong action method của bạn
            

        }
        public IActionResult FilterResult(string address, int rating)
        {
            // Xử lý và kết hợp dữ liệu từ hai form
            var combinedData = new CombinedData { Address = address, Rating = rating };

            // Trả về view với dữ liệu kết hợp
            return View(combinedData);
        }

        // GET: TblDatphongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDatphong = await _context.TblDatphongs
                .Include(t => t.IdPhongNavigation)
                .Include(t => t.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdMadatphong == id);
            if (tblDatphong == null)
            {
                return NotFound();
            }

            return View(tblDatphong);
        }

        // GET: TblDatphongs/Create
        public IActionResult Create()
        {
            ViewData["IdPhong"] = new SelectList(_context.TblUsers, "IdPhong", "IdPhong");
            ViewData["IdUser"] = new SelectList(_context.TblUsers, "IdUser", "IdUser");
            return View();
        }

        // POST: TblDatphongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMadatphong,IdUser,IdPhong,DNgaycheckin,DNgaycheckout,SHinhthucthanhtoan")] TblDatphong tblDatphong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblDatphong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPhong"] = new SelectList(_context.TblPhongs, "IdPhong", "IdPhong", tblDatphong.IdPhong);
            ViewData["IdUser"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDatphong.IdUser);
            return View(tblDatphong);
        }

        // GET: TblDatphongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDatphong = await _context.TblDatphongs.FindAsync(id);
            if (tblDatphong == null)
            {
                return NotFound();
            }
            ViewData["IdPhong"] = new SelectList(_context.TblPhongs, "IdPhong", "IdPhong", tblDatphong.IdPhong);
            ViewData["IdUser"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDatphong.IdUser);
            return View(tblDatphong);
        }

        // POST: TblDatphongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMadatphong,IdUser,IdPhong,DNgaycheckin,DNgaycheckout,SHinhthucthanhtoan")] TblDatphong tblDatphong)
        {
            if (id != tblDatphong.IdMadatphong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblDatphong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblDatphongExists(tblDatphong.IdMadatphong))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPhong"] = new SelectList(_context.TblPhongs, "IdPhong", "IdPhong", tblDatphong.IdPhong);
            ViewData["IdUser"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDatphong.IdUser);
            return View(tblDatphong);
        }

        // GET: TblDatphongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDatphong = await _context.TblDatphongs
                .Include(t => t.IdPhongNavigation)
                .Include(t => t.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdMadatphong == id);
            if (tblDatphong == null)
            {
                return NotFound();
            }

            return View(tblDatphong);
        }

        // POST: TblDatphongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblDatphong = await _context.TblDatphongs.FindAsync(id);
            if (tblDatphong != null)
            {
                _context.TblDatphongs.Remove(tblDatphong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblDatphongExists(int id)
        {
            return _context.TblDatphongs.Any(e => e.IdMadatphong == id);
        }
    }
}
