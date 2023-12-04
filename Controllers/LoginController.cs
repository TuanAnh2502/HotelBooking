using HotelBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HotelBooking.Controllers {
[Area("User")]
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly HotelBookingContext _context;
    public LoginController(ILogger<LoginController> logger, HotelBookingContext context)
    {
        _logger = logger;
        _context = context;
    }
    [Route("/Login")]
    public IActionResult Index()
    {
        return View("~/Views/Login/Login.cshtml");
    }

    [Route("Login/Singin")]
    [HttpPost]
    public async Task<IActionResult> Singin(TblUser user)
    {
        string name = Request.Form["Email"];
        string password = Request.Form["Password"];

        var member = await _context.TblUsers.FirstOrDefaultAsync(m => m.SEmail == name && m.SMatkhau == password);

            
            if (member != null)
        {

            HttpContext.Session.SetInt32("UserId", member.IdUser);
            HttpContext.Session.SetString("UserName", member.STendaydu.ToString());

                TempData["SuccessMessage"] = "Đăng nhập thành công";
                TempData["UserId"] = member.IdUser;
                TempData["UserName"] = member.STendaydu;
                return RedirectToAction("Index", "Home");
                

        }
        else
        {
            TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác";//Thông báo ra màn hình thánh công

            return RedirectToAction("Index");
        }

    }
    [Route("/Login/Register")]
    public IActionResult Register()
    {
        return View("~/Views/Login/Register.cshtml");
    }
    [Route("Login/Singup")]
    [HttpPost]
    public async Task<IActionResult> Singup(TblUser user)
    {
        string email = Request.Form["Email"];
        string password = Request.Form["Password"];
        string phone = Request.Form["Phone"];
        string tendaydu = Request.Form["Name"];

            _context.TblUsers.Add(new TblUser
        {
            STendaydu = tendaydu,
            SEmail = email,
            SSdt = phone,
            SMatkhau = password
        });
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Đăng ký thành công";//Thông báo ra màn hình thánh công

        return RedirectToAction("Index");
    }


} }