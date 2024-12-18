using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Diagnostics;

namespace B221200015_WP_ODEV.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet] 
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                Debug.WriteLine($"Kullanıcı Adı: {username}");
                Debug.WriteLine($"Şifre: {password}");

                if (username == "admin" && password == "12345")
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Admin")
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Admin", "Admin");
                }

                ViewBag.Error = "Geçersiz kullanıcı adı veya şifre.";
                return View();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hata: {ex.Message}");
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}
