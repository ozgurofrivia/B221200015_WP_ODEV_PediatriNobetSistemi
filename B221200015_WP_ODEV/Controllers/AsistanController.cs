using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace B221200015_WP_ODEV.Controllers
{
    public class AsistanController : Controller
    {
        private readonly DatabaseContext _context;

        public AsistanController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Asistan()
        {
            var asistanlar = _context.Asistanlar.Include(h => h.Bolum).ToList();
            return View(asistanlar);
        }

        // Asistanların Listelenmesi
        public IActionResult AsistanList()
        {
            var asistanlar = _context.Asistanlar.Include(h => h.Bolum).ToList();
            return View(asistanlar);
        }

        // Yeni Asistan Ekleme - GET
        [HttpGet]
        public IActionResult AsistanAdd()
        {
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Asistan Ekleme - POST
        [HttpPost]
        public IActionResult AsistanAdd(Asistan asistan)
        {
            // Yeni kayıt için ID sıfırlanır
            asistan.Id = 0;

            // Kayıt eklenir
            _context.Asistanlar.Add(asistan);
            _context.SaveChanges();

            // Listeye yönlendirme yapılır
            return RedirectToAction("AsistanList");
        }

        // Asistan Güncelleme - GET
        [HttpGet]
        public IActionResult AsistanUpdate(int id)
        {
            var asistan = _context.Asistanlar.Find(id);
            if (asistan == null) return NotFound();

            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View(asistan);
        }

        // Asistan Güncelleme - POST
        [HttpPost]
        public IActionResult AsistanUpdate(Asistan asistan)
        {
            _context.Asistanlar.Update(asistan);
            _context.SaveChanges();
            return RedirectToAction("AsistanList");
        }

        // Asistan Silme - GET (Onay Sayfası)
        [HttpGet]
        public IActionResult AsistanDelete(int id)
        {
            var asistan = _context.Asistanlar.Include(h => h.Bolum).FirstOrDefault(h => h.Id == id);
            if (asistan == null) return NotFound();
            return View(asistan);
        }

        // Asistan Silme - POST
        [HttpPost, ActionName("AsistanDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var asistan = _context.Asistanlar
                .Include(h => h.Randevular) // Randevular tablosundaki ilişkili verileri dahil et
                .FirstOrDefault(h => h.Id == id);

            if (asistan != null)
            {
                // Önce ilişkili randevuları sil
                _context.Randevular.RemoveRange(asistan.Randevular);

                // Daha sonra Asistan kaydını sil
                _context.Asistanlar.Remove(asistan);
                _context.SaveChanges();

                // Kimlik sıfırlama işlemi
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Asistanlar', RESEED, 0)");
            }

            return RedirectToAction("AsistanList");
        }

    }
}
