using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B221200015_WP_ODEV.Controllers
{
    public class HocaController : Controller
    {
        private readonly DatabaseContext _context;

        public HocaController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Hoca(string searchTerm, string sortOrder)
        { 
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocalar = hocalar.Where(h =>
                    h.Ad.Contains(searchTerm) ||
                    h.Soyad.Contains(searchTerm) ||
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            switch (sortOrder)
            {
                case "name_desc":
                    hocalar = hocalar.OrderByDescending(h => h.Ad);
                    break;
                case "department":
                    hocalar = hocalar.OrderBy(h => h.Bolum.BolumAdi);
                    break;
                case "department_desc":
                    hocalar = hocalar.OrderByDescending(h => h.Bolum.BolumAdi);
                    break;
                default:
                    hocalar = hocalar.OrderBy(h => h.Ad); // Varsayılan sıralama: ad'a göre
                    break;
            }

            return View(hocalar.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult HocaList(string searchTerm)
        {
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum) 
                .OrderBy(h => h.Bolum.BolumAdi)  
                .AsQueryable();       

            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocalar = hocalar.Where(h =>
                    h.Ad.Contains(searchTerm) ||               
                    h.Soyad.Contains(searchTerm) ||            
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            return View(hocalar.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaAdd()
        {
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult HocaAdd(Hoca hoca, IFormFile Resim)
        {
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hocalar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hocalar/" + Resim.FileName;
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(hoca);
            }

            _context.Hocalar.Add(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaUpdate(int id)
        {
            var hoca = _context.Hocalar.Find(id);
            if (hoca == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(hoca);
        }

        [HttpPost]
        public IActionResult HocaUpdate(Hoca hoca, IFormFile Resim)
        {
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hocalar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hocalar/" + Resim.FileName;
            }

            _context.Hocalar.Update(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaDelete(int id)
        {
            var hoca = _context.Hocalar.Include(h => h.Bolum).FirstOrDefault(h => h.Id == id);
            if (hoca == null) return NotFound();
            return View(hoca);
        }

        [HttpPost, ActionName("HocaDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var hoca = _context.Hocalar
                .Include(h => h.Randevular)
                .FirstOrDefault(h => h.Id == id);

            if (hoca != null)
            {
                _context.Randevular.RemoveRange(hoca.Randevular);
                _context.Hocalar.Remove(hoca);
                _context.SaveChanges();
            }

            return RedirectToAction("HocaList");
        }
    }
}
