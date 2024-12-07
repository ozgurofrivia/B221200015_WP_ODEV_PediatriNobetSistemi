using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace B221200015_WP_ODEV.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        // Admin Paneli Ana Sayfası
        public IActionResult Admin()
        {
            return View();
        }

        
    }
}
