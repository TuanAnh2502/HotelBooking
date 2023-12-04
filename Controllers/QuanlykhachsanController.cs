using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class QuanlykhachsanController : Controller
    { 
    private readonly HotelBookingContext _context;

    public QuanlykhachsanController(HotelBookingContext context)
    {
        _context = context;
    }

    // GET: TblKhachSans
    public async Task<IActionResult> Index(int? id)
    {
            if (id == null)
            {
                return View("~/Views/Login/Login.cshtml");
            }

            var khachSanList = await _context.TblKhachSans
                .Where(khachSan => khachSan.IdUser == id)
                .ToListAsync();
            return View(khachSanList);
        }

    // GET: TblKhachSans/Details/5
    public async Task<IActionResult> Details(int? id)//laays danh sach cac phong
    {
        if (id == null)
        {
            return NotFound();
        }
            var tblPhong = await _context.TblPhongs
             .Include(t => t.IdKhachsanNavigation)
             .Where(t => t.IdKhachsan == id)
             .ToListAsync();
            ViewBag.tenkhachsan = await _context.TblKhachSans
            .Where(m => m.IdKhachsan == id)
            .Select(m => m.STenkhachsan)
            .FirstOrDefaultAsync();
            if (tblPhong == null)
        {
            return NotFound();
        }

        return View(tblPhong);
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
    public async Task<IActionResult> Create([Bind("IdKhachsan,STenkhachsan,SDiachi,SMotakhachsan,SAnhkhachsan,SDanhgia,IdUser")] TblKhachSan tblKhachSan)
    {
            if (ViewBag.UserId == null)
            {
                // Nếu ViewBag.UserId không có giá trị, hiển thị thông báo không tìm thấy người dùng
                ModelState.AddModelError("UserId", "Không tìm thấy người dùng.");
                return View(tblKhachSan);
            }

            // Nếu ViewBag.UserId có giá trị, tiếp tục lưu dữ liệu vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                tblKhachSan.IdUser = (int)ViewBag.UserId; // Gán giá trị cho IdUser từ ViewBag

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
