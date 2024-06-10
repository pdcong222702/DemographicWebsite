using AspNetCoreHero.ToastNotification.Abstractions;
using Demographic_Website.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demographic_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthencationController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DemoGraphicContext _context;
        public INotyfService _notyfService { get; }

        public AuthencationController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, DemoGraphicContext context, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _notyfService = notyfService;
        }


        public IActionResult RegisterAccont()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterAccont(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                if (CheckAccountRegisterInPopulation(user.IdentificationCode))
                {
                    if (CheckAccountRegisterInUser(user.IdentificationCode))
                    {
                        ViewBag.Error = "CCCD/CMND đã tồn tại trong hệ thống";
                        return View(user);
                    }
                    else
                    {
                        user.UserName = user.IdentificationCode;
                        _context.Add(user);
                        _context.SaveChanges();
                        _notyfService.Success("Bạn đăng ký tài khoản thành công");
                        return RedirectToAction("Index", "RecedentceBooklet");
                    }
                }
            }
            return View(user);
        }

        private bool CheckAccountRegisterInPopulation(string IdentificationCode)
        {
            return _context.Populations.Any(p => p.CitizenIdentificationCard == IdentificationCode);
        }
        private bool CheckAccountRegisterInUser(string IdentificationCode)
        {
            return _context.Users.Any(p => p.IdentificationCode == IdentificationCode);
        }

        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("IdentificationCode") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "RecedentceBooklet");
            }

        }

        [HttpPost]
        public IActionResult Login(ApplicationUser userLogin)
        {
            if (HttpContext.Session.GetString("IdentificationCode") == null)
            {
                var user = _context.Users.Where(u => u.IdentificationCode.Equals(userLogin.IdentificationCode) && u.Password.Equals(userLogin.Password)).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Session.SetString("IdentificationCode", user.IdentificationCode.ToString());
                    return RedirectToAction("Index", "RecedentceBooklet");
                }
                else
                {
                    ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("IdentificationCode");
            return RedirectToAction("Login", "Authencation");
        }
    }
}
