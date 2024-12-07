using Microsoft.AspNetCore.Mvc;
using System.Linq;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.EntityFrameworkCore;


namespace B221200015_WP_ODEV.Controllers
{
    public class RandevuController : Controller
    {
        private readonly DatabaseContext _context;

        public RandevuController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Randevu()
        {
            var randevular = _context.Randevular.Include(a=>a.Asistan).ToList();
            randevular = _context.Randevular.Include(h => h.Hoca).ToList();
            return View(randevular);
        }

        public IActionResult RandevuList()
        {
            var randevular = _context.Randevular.Include(a => a.Asistan).ToList();
            randevular = _context.Randevular.Include(h => h.Hoca).ToList();
            return View(randevular);
        }

        [HttpGet]
        public IActionResult RandevuAdd()
        {
            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Hocalar = _context.Hocalar.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RandevuAdd(Randevu randevu)
        {
            _context.Randevular.Add(randevu);
            _context.SaveChanges();
            return RedirectToAction("RandevuList");
        }

        [HttpGet]
        public IActionResult RandevuUpdate(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null) return NotFound();

            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Hocalar = _context.Hocalar.ToList(); ;
            return View(randevu);
        }

        // Hoca Güncelleme - POST
        [HttpPost]
        public IActionResult RandevuUpdate(Randevu randevu)
        {

            _context.Randevular.Update(randevu);
            _context.SaveChanges();
            return RedirectToAction("RandevuList");
        }

        [HttpGet]
        public IActionResult RandevuDelete(int id)
        {
            var randevu = _context.Randevular.Include(a => a.Asistan).FirstOrDefault(a => a.Id == id);
            randevu = _context.Randevular.Include(h => h.Hoca).FirstOrDefault(h => h.Id == id);
            if (randevu == null) return NotFound();
            return View(randevu);
        }

        // Hoca Silme - POST
        [HttpPost, ActionName("RandevuDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                _context.SaveChanges();
                _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Randevular', RESEED, 0)");
            }
            return RedirectToAction("RandevuList");

        }
    }
}
