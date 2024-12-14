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
            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult NobetAdd(Nobet nobet)
        {
            if (nobet == null)
            {
                throw new Exception("Nobet nesnesi null.");
            }

            // Çakışma kontrolü
            var mevcutNobet = _context.Nobetler
                .Where(n => n.AsistanId == nobet.AsistanId)
                .Where(n =>
                    (nobet.BaslamaTarihi >= n.BaslamaTarihi && nobet.BaslamaTarihi <= n.BitisTarihi) ||
                    (nobet.BitisTarihi >= n.BaslamaTarihi && nobet.BitisTarihi <= n.BitisTarihi))
                .FirstOrDefault();

            if (mevcutNobet != null)
            {
                // Hata mesajını ModelState'e ekleyin
                ModelState.AddModelError("", "Bu asistan belirtilen tarihler arasında zaten başka bir nöbete atanmış.");

                // ViewBag ile gerekli listeleri tekrar ekleyin
                ViewBag.Asistanlar = _context.Asistanlar.ToList();
                ViewBag.Bolumler = _context.Bolumler.ToList();

                // Kullanıcıya mevcut model ile tekrar aynı sayfayı döndürün
                return View(nobet);
            }

            // Nöbeti ekle
            _context.Nobetler.Add(nobet);
            _context.SaveChanges();
            return RedirectToAction(nameof(NobetList));
        }



    }
}
