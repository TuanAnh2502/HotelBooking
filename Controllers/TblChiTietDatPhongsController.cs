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
    public class TblChiTietDatPhongsController : Controller
    {
        private readonly HotelBookingContext _context;

        public TblChiTietDatPhongsController(HotelBookingContext context)
        {
            _context = context;
        }

        // GET: TblChiTietDatPhongs
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
            var hotelBookingContext = _context.TblChiTietDatPhongs.Include(t => t.IdMadatphongNavigation);
            return View(await hotelBookingContext.ToListAsync());
        }

        // GET: TblChiTietDatPhongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChiTietDatPhong = await _context.TblChiTietDatPhongs
                .Include(t => t.IdMadatphongNavigation)
                .FirstOrDefaultAsync(m => m.IdChitietdatphong == id);
            if (tblChiTietDatPhong == null)
            {
                return NotFound();
            }

            return View(tblChiTietDatPhong);
        }

        // GET: TblChiTietDatPhongs/Create
        public IActionResult Create()
        {
            ViewData["IdMadatphong"] = new SelectList(_context.TblDatphongs, "IdMadatphong", "IdMadatphong");
            return View();
        }

        // POST: TblChiTietDatPhongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChitietdatphong,IdMadatphong,STenuser,SEmailkhachhang,SSdtkhachhang,SChuthich")] TblChiTietDatPhong tblChiTietDatPhong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblChiTietDatPhong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMadatphong"] = new SelectList(_context.TblDatphongs, "IdMadatphong", "IdMadatphong", tblChiTietDatPhong.IdMadatphong);
            return View(tblChiTietDatPhong);
        }

        // GET: TblChiTietDatPhongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChiTietDatPhong = await _context.TblChiTietDatPhongs.FindAsync(id);
            if (tblChiTietDatPhong == null)
            {
                return NotFound();
            }
            ViewData["IdMadatphong"] = new SelectList(_context.TblDatphongs, "IdMadatphong", "IdMadatphong", tblChiTietDatPhong.IdMadatphong);
            return View(tblChiTietDatPhong);
        }

        // POST: TblChiTietDatPhongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdChitietdatphong,IdMadatphong,STenuser,SEmailkhachhang,SSdtkhachhang,SChuthich")] TblChiTietDatPhong tblChiTietDatPhong)
        {
            if (id != tblChiTietDatPhong.IdChitietdatphong)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblChiTietDatPhong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblChiTietDatPhongExists(tblChiTietDatPhong.IdChitietdatphong))
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
            ViewData["IdMadatphong"] = new SelectList(_context.TblDatphongs, "IdMadatphong", "IdMadatphong", tblChiTietDatPhong.IdMadatphong);
            return View(tblChiTietDatPhong);
        }

        // GET: TblChiTietDatPhongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblChiTietDatPhong = await _context.TblChiTietDatPhongs
                .Include(t => t.IdMadatphongNavigation)
                .FirstOrDefaultAsync(m => m.IdChitietdatphong == id);
            if (tblChiTietDatPhong == null)
            {
                return NotFound();
            }

            return View(tblChiTietDatPhong);
        }

        // POST: TblChiTietDatPhongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblChiTietDatPhong = await _context.TblChiTietDatPhongs.FindAsync(id);
            if (tblChiTietDatPhong != null)
            {
                _context.TblChiTietDatPhongs.Remove(tblChiTietDatPhong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblChiTietDatPhongExists(int id)
        {
            return _context.TblChiTietDatPhongs.Any(e => e.IdChitietdatphong == id);
        }
    }
}
