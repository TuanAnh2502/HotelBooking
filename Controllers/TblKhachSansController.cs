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
    public class TblKhachSansController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblKhachSansController(HotelBookingContext context)
        {
            _context = context;
        }

        // GET: TblKhachSans
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            return View(await _context.TblKhachSans.ToListAsync());
        }
        public IActionResult GetFilteredHotels(int rating)
        {
            // Xử lý lọc khách sạn dựa trên rating, và trả về một phần của View
            var filteredHotels = _context.TblKhachSans.Where(h => h.SDanhgia >= rating).ToList();// Code để lấy danh sách khách sạn theo điều kiện rating
            
            return PartialView("_FilteredHotelsPartialView", filteredHotels);
        }
        // GET: TblKhachSans/Details/5
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

        // GET: TblKhachSans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblKhachSans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKhachsan,STenkhachsan,SDiachi,SMotakhachsan,SAnhkhachsan,SDanhgia")] TblKhachSan tblKhachSan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblKhachSan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblKhachSan);
        }

        // GET: TblKhachSans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKhachSan = await _context.TblKhachSans.FindAsync(id);
            if (tblKhachSan == null)
            {
                return NotFound();
            }
            return View(tblKhachSan);
        }

        // POST: TblKhachSans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKhachsan,STenkhachsan,SDiachi,SMotakhachsan,SAnhkhachsan,SDanhgia")] TblKhachSan tblKhachSan)
        {
            if (id != tblKhachSan.IdKhachsan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblKhachSan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblKhachSanExists(tblKhachSan.IdKhachsan))
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
            return View(tblKhachSan);
        }

        // GET: TblKhachSans/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: TblKhachSans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblKhachSan = await _context.TblKhachSans.FindAsync(id);
            if (tblKhachSan != null)
            {
                _context.TblKhachSans.Remove(tblKhachSan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblKhachSanExists(int id)
        {
            return _context.TblKhachSans.Any(e => e.IdKhachsan == id);
        }
    }
}
