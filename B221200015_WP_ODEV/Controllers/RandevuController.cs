using Microsoft.AspNetCore.Mvc;
using System.Linq;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


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

        [Authorize(Roles = "Admin")]
        public IActionResult RandevuList()
        {
            var randevular = _context.Randevular.Include(a => a.Asistan).ToList();
            randevular = _context.Randevular.Include(h => h.Hoca).ToList();
            return View(randevular);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RandevuAdd()
        {
            ViewBag.Asistanlar = _context.Asistanlar.ToList();
            ViewBag.Hocalar = _context.Hocalar.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult GetHocaMusaitlikler(int hocaId)
        {
            var musaitlikler = _context.HocaMusaitlikler
                .Where(m => m.HocaId == hocaId && !_context.Randevular
                    .Any(r => r.Tarih == m.Tarih && r.Saat == m.Saat))
                .ToList();

            return Json(musaitlikler);
        }

        [HttpPost]
        public IActionResult RandevuAdd(Randevu randevu)
        {
            // müsaitlik kontrolü
            var musaitlik = _context.HocaMusaitlikler
                .FirstOrDefault(m => m.HocaId == randevu.HocaId
                                  && m.Tarih == randevu.Tarih
                                  && m.Saat == randevu.Saat);

            if (musaitlik == null)
            {
                ModelState.AddModelError("", "Seçilen tarih ve saat artık müsait değil.");
                ViewBag.Asistanlar = _context.Asistanlar.ToList();
                ViewBag.Hocalar = _context.Hocalar.ToList();
                return View(randevu);
            }

            //if (!ModelState.IsValid)
            //{
            //    // Bölüm listesini ViewBag ile tekrar göndermek
            //    ViewBag.Asistanlar = _context.Asistanlar.ToList();
            //    ViewBag.Hocalar = _context.Hocalar.ToList();
            //    return View(randevu);
            //}

            _context.Randevular.Add(randevu);
            _context.SaveChanges();
            return RedirectToAction("RandevuList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult RandevuDelete(int id)
        {
            var randevu = _context.Randevular.Include(a => a.Asistan).FirstOrDefault(a => a.Id == id);
            randevu = _context.Randevular.Include(h => h.Hoca).FirstOrDefault(h => h.Id == id);
            if (randevu == null) return NotFound();
            return View(randevu);
        }

        [HttpPost, ActionName("RandevuDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu != null)
            {
                _context.Randevular.Remove(randevu);
                _context.SaveChanges();
            }
            return RedirectToAction("RandevuList");
        }
    }
}
