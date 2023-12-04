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
    public class TblPhongsController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblPhongsController(HotelBookingContext context)
        {
            _context = context;
        }

        // GET: TblPhongs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            var hotelBookingContext = _context.TblPhongs.Include(t => t.IdKhachsanNavigation);
            return View(await hotelBookingContext.ToListAsync());
        }

        // GET: TblPhongs/Details/5
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

        // GET: TblPhongs/Create
        public IActionResult Create(int ? id)
        {
            ViewBag.idkhachsan = id;
           /* ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan");*/
            return View();
        }

        // POST: TblPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPhong,IdKhachsan,SKieuPhong,FGiaphong,SPhongcontrong,SMotaphong,STiennghi,SAnhphong")] TblPhong tblPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblPhong);
                await _context.SaveChangesAsync();
                // Lấy IdKhachsan sau khi đã lưu vào cơ sở dữ liệu
                int idKhachsan = (int)tblPhong.IdKhachsan;

                // Chuyển hướng đến trang Quanlykhacsan/Detail/id
                return RedirectToAction( "Details", "Quanlykhachsan", new { id = idKhachsan });
            }
            /*ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblPhong.IdKhachsan);*/
            return View(tblPhong);
        }

        // GET: TblPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblPhong = await _context.TblPhongs.FindAsync(id);
            if (tblPhong == null)
            {
                return NotFound();
            }
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblPhong.IdKhachsan);
            return View(tblPhong);
        }

        // POST: TblPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPhong,IdKhachsan,SKieuPhong,FGiaphong,SPhongcontrong,SMotaphong,STiennghi,SAnhphong")] TblPhong tblPhong)
        {
            if (id != tblPhong.IdPhong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblPhongExists(tblPhong.IdPhong))
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
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblPhong.IdKhachsan);
            return View(tblPhong);
        }

        // GET: TblPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: TblPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblPhong = await _context.TblPhongs.FindAsync(id);
            if (tblPhong != null)
            {
                _context.TblPhongs.Remove(tblPhong);
            }

            await _context.SaveChangesAsync();
            int idKhachsan = (int)tblPhong.IdKhachsan;

            // Chuyển hướng đến trang Quanlykhacsan/Detail/id
            return RedirectToAction("Details", "Quanlykhachsan", new { id = idKhachsan });
        }

        private bool TblPhongExists(int id)
        {
            return _context.TblPhongs.Any(e => e.IdPhong == id);
        }
    }
}
