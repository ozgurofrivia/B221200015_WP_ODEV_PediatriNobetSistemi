using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Models;
using B221200015_WP_ODEV.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace B221200015_WP_ODEV.Controllers
{
    public class NobetController : Controller
    {
        private readonly DatabaseContext _context;

        public NobetController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult NobetList()
        {
            var nobetler = _context.Nobetler.Include(n => n.Asistan).Include(n => n.Bolum).ToList();
            return View(nobetler);
        }

        [HttpGet]
        public IActionResult NobetAdd()
        {
            var asistanlar = _context.Asistanlar.ToList();
            var bolumler = _context.Bolumler.ToList();

            ViewBag.Asistanlar = asistanlar;
            ViewBag.Bolumler = bolumler;

            if (asistanlar == null || !asistanlar.Any())
            {
                throw new Exception("Asistanlar listesi boş veya yüklenemedi.");
            }

            if (bolumler == null || !bolumler.Any())
            {
                throw new Exception("Bolumler listesi boş veya yüklenemedi.");
            }

            return View();
        }

        [HttpPost]
        public IActionResult NobetAdd(Nobet nobet)
        {
            if (nobet == null)
            {
                throw new Exception("Nobet nesnesi null.");
            }

            _context.Nobetler.Add(nobet);
            _context.SaveChanges();
            return RedirectToAction(nameof(NobetList));
        }
    }
}
