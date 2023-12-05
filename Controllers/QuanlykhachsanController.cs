using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Text;

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

            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
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

            ViewBag.idkhachsan = id;

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
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }

            // Nếu ViewBag.UserId có giá trị, tiếp tục lưu dữ liệu vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                tblKhachSan.IdUser= ViewBag.UserId;
                _context.Add(tblKhachSan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Quanlykhachsan", new {id=tblKhachSan.IdUser });
            }

            return View(tblKhachSan);
        }

    // GET: TblKhachSans/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) && HttpContext.Session.TryGetValue("UserName", out var userName))
            {
                var userIdString = HttpContext.Session.GetInt32("UserId");
                var userNameString = Encoding.UTF8.GetString(userName);
                ViewBag.UserId = userIdString;
                ViewBag.UserName = userNameString;
            }
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
                // Bước 1: Lấy danh sách các phòng có IdKhachsan bằng id
                var phongs = _context.TblPhongs.Where(p => p.IdKhachsan == id).ToList();

                var danhgias = _context.TblDanhGia.Where(dg => dg.IdKhachsan == id).ToList();
                // Bước 2: Lấy danh sách các IdPhong từ các phòng có IdKhachsan bằng id
                var idPhongs = phongs.Select(p => p.IdPhong).ToList();

                // Bước 3: Lấy danh sách các đặt phòng có IdPhong nằm trong danh sách idPhongs
                var datPhongs = _context.TblDatphongs.Where(dp => idPhongs.Contains((int)dp.IdPhong)).ToList();

                var id_Madatphongs = datPhongs.Select(p => p.IdMadatphong).ToList();
                // Bước 4: Lấy danh sách các chi tiết đặt phòng có ID_Madatphong nằm trong danh sách idPhongs
                var chiTietDatPhongs = _context.TblChiTietDatPhongs.Where(ct => id_Madatphongs.Contains((int)ct.IdMadatphong)).ToList();

                _context.TblDanhGia.RemoveRange(danhgias);

                // Bước 5: Xóa các chi tiết đặt phòng
                _context.TblChiTietDatPhongs.RemoveRange(chiTietDatPhongs);

                // Bước 4: Xóa các đặt phòng
                _context.TblDatphongs.RemoveRange(datPhongs);

                // Bước 2: Xóa các phòng
                _context.TblPhongs.RemoveRange(phongs);

                // Bước 1: Xóa khách sạn
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
