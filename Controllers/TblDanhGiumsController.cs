using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;
using System.Text;

namespace HotelBooking.wwwroot
{
    public class TblDanhGiumsController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblDanhGiumsController(HotelBookingContext context)
        {
            _context = context;
        }

        // GET: TblDanhGiums
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            ViewBag.IsLoggedIn = User.Identity.IsAuthenticated;
            var hotelBookingContext = _context.TblDanhGia.Include(t => t.IdKhachhangNavigation).Include(t => t.IdKhachsanNavigation);
            return View(await hotelBookingContext.ToListAsync());
        }

        // GET: TblDanhGiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDanhGium = await _context.TblDanhGia
                .Include(t => t.IdKhachhangNavigation)
                .Include(t => t.IdKhachsanNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhgia == id);
            if (tblDanhGium == null)
            {
                return NotFound();
            }

            return View(tblDanhGium);
        }

        // GET: TblDanhGiums/Create
        public IActionResult Create()
        {
            ViewData["IdKhachhang"] = new SelectList(_context.TblUsers, "IdUser", "IdUser");
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan");
            return View();
        }

        // POST: TblDanhGiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDanhgia,IdKhachsan,IdKhachhang,ISosaodanhgia,SNoidung")] TblDanhGium tblDanhGium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblDanhGium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachhang"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDanhGium.IdKhachhang);
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblDanhGium.IdKhachsan);
            return View(tblDanhGium);
        }

        // GET: TblDanhGiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDanhGium = await _context.TblDanhGia.FindAsync(id);
            if (tblDanhGium == null)
            {
                return NotFound();
            }
            ViewData["IdKhachhang"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDanhGium.IdKhachhang);
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblDanhGium.IdKhachsan);
            return View(tblDanhGium);
        }

        // POST: TblDanhGiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDanhgia,IdKhachsan,IdKhachhang,ISosaodanhgia,SNoidung")] TblDanhGium tblDanhGium)
        {
            if (id != tblDanhGium.IdDanhgia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblDanhGium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblDanhGiumExists(tblDanhGium.IdDanhgia))
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
            ViewData["IdKhachhang"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDanhGium.IdKhachhang);
            ViewData["IdKhachsan"] = new SelectList(_context.TblKhachSans, "IdKhachsan", "IdKhachsan", tblDanhGium.IdKhachsan);
            return View(tblDanhGium);
        }

        // GET: TblDanhGiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblDanhGium = await _context.TblDanhGia
                .Include(t => t.IdKhachhangNavigation)
                .Include(t => t.IdKhachsanNavigation)
                .FirstOrDefaultAsync(m => m.IdDanhgia == id);
            if (tblDanhGium == null)
            {
                return NotFound();
            }

            return View(tblDanhGium);
        }

        // POST: TblDanhGiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblDanhGium = await _context.TblDanhGia.FindAsync(id);
            if (tblDanhGium != null)
            {
                _context.TblDanhGia.Remove(tblDanhGium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblDanhGiumExists(int id)
        {
            return _context.TblDanhGia.Any(e => e.IdDanhgia == id);
        }
    }
}
