using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
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

        public IActionResult Hoca()
        {
            var hocalar = _context.Hocalar.Include(h => h.Bolum).ToList();
            return View(hocalar);
        }

        // Hocaların Listelenmesi
        public IActionResult HocaList()
        {
            var hocalar = _context.Hocalar.Include(h => h.Bolum).ToList();
            return View(hocalar);
        }

        // Yeni Hoca Ekleme - GET
        [HttpGet]
        public IActionResult HocaAdd()
        {
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Hoca Ekleme - POST
        [HttpPost]
        public IActionResult HocaAdd(Hoca hoca)
        {

            // Kayıt eklenir
            _context.Hocalar.Add(hoca);
            _context.SaveChanges();

            // Listeye yönlendirme yapılır
            return RedirectToAction("HocaList");
        }

        // Hoca Güncelleme - GET
        [HttpGet]
        public IActionResult HocaUpdate(int id)
        {
            var hoca = _context.Hocalar.Find(id);
            if (hoca == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(hoca);
        }

        // Hoca Güncelleme - POST
        [HttpPost]
        public IActionResult HocaUpdate(Hoca hoca)
        {
            _context.Hocalar.Update(hoca);
            _context.SaveChanges();
            return RedirectToAction("HocaList");
        }

        // Hoca Silme - GET (Onay Sayfası)
        [HttpGet]
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

                // Kimlik sıfırlama işlemi
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Hocalar', RESEED, 0)");
            }

            return RedirectToAction("HocaList");
        }



    }
}
