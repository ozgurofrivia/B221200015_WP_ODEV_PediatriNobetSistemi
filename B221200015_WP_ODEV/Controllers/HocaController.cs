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
            // Hoca bilgilerini bölümleriyle birlikte sorgulama
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum)
                .AsQueryable();

            // Eğer arama terimi sağlanmışsa, filtreleme uygula
            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocalar = hocalar.Where(h =>
                    h.Ad.Contains(searchTerm) ||
                    h.Soyad.Contains(searchTerm) ||
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            // Sıralama işlemi
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

            // Sonuçları listeye çevir ve görünümde göster
            return View(hocalar.ToList());
        }

        // Hocaların Listelenmesi
        [Authorize(Roles = "Admin")]
        public IActionResult HocaList(string searchTerm)
        {
            var hocalar = _context.Hocalar
                .Include(h => h.Bolum) 
                .OrderBy(h => h.Bolum.BolumAdi)  
                .AsQueryable();       

            // Eğer arama terimi sağlanmışsa, filtreleme uygula
            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocalar = hocalar.Where(h =>
                    h.Ad.Contains(searchTerm) ||               
                    h.Soyad.Contains(searchTerm) ||            
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            // Sonuçları listeye çevir ve görünümde göster
            return View(hocalar.ToList());
        }

        // Yeni Hoca Ekleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaAdd()
        {
            // Bölüm listesini ViewBag ile gönderiyoruz
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Hoca Ekleme - POST
        [HttpPost]
        public IActionResult HocaAdd(Hoca hoca, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hocalar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hocalar/" + Resim.FileName;
            }
            // Eğer ModelState geçerli değilse, formu tekrar gösteriyoruz
            if (!ModelState.IsValid)
            {
                // Bölüm listesini ViewBag ile tekrar göndermek
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(hoca);
            }
            // Yeni hocaı ekliyoruz
            _context.Hocalar.Add(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        // Hoca Güncelleme - GET
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaUpdate(int id)
        {
            var hoca = _context.Hocalar.Find(id);
            if (hoca == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(hoca);
        }

        // Hoca Güncelleme - POST
        [HttpPost]
        public IActionResult HocaUpdate(Hoca hoca, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "hocalar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                hoca.Resim = "/images/hocalar/" + Resim.FileName;
            }

            // Hoca bilgilerini güncelliyoruz
            _context.Hocalar.Update(hoca);
            _context.SaveChanges();

            return RedirectToAction("HocaList");
        }

        // Hoca Silme - GET (Onay Sayfası)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaDelete(int id)
        {
            var hoca = _context.Hocalar.Include(h => h.Bolum).FirstOrDefault(h => h.Id == id);
            if (hoca == null) return NotFound();
            return View(hoca);
        }

        // Hoca Silme - POST
        [HttpPost, ActionName("HocaDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var hoca = _context.Hocalar
                .Include(h => h.Randevular) // Randevular tablosundaki ilişkili verileri dahil et
                .FirstOrDefault(h => h.Id == id);

            if (hoca != null)
            {
                // Önce ilişkili randevuları sil
                _context.Randevular.RemoveRange(hoca.Randevular);

                // Daha sonra Hoca kaydını sil
                _context.Hocalar.Remove(hoca);
                _context.SaveChanges();

            }

            return RedirectToAction("HocaList");
        }



    }
}
