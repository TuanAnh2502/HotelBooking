using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBooking.Models;

namespace HotelBooking.Controllers
{
    public class TblDatphongsController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblDatphongsController(HotelBookingContext context)
        {
            _context = context;
        }

        // GET: TblDatphongs
        public async Task<IActionResult> Index()
        {
            
            var hotelslist = await _context.TblKhachSans
                                   .Include(h => h.IdUserNavigation)
                                   .ToListAsync();
          

            return View(hotelslist);
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
        public IActionResult Create(int? idPhong)
        {
            ViewBag.IdPhongvalue = idPhong;
/*            if (TempData.TryGetValue("UserId", out var userId) && TempData.TryGetValue("UserName", out var userName))
            {
                ViewBag.UserId = userId;
            }
            ViewData["IdUser"] = TempData.TryGetValue("UserId", out var userId)*/ /*new SelectList(_context.TblUsers, "IdUser", "IdUser")*/;
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
            /*ViewData["IdPhong"] = new SelectList(_context.TblPhongs, "IdPhong", "IdPhong", tblDatphong.IdPhong);
            ViewData["IdUser"] = new SelectList(_context.TblUsers, "IdUser", "IdUser", tblDatphong.IdUser);*/
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
