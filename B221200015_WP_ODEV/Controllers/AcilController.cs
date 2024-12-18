using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Models;
using B221200015_WP_ODEV.Helper;
using B221200015_WP_ODEV.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace B221200015_WP_ODEV.Controllers
{
    public class AcilController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IMailHelper _mailHelper;

        public AcilController(DatabaseContext context, IMailHelper mailHelper)
        {
            _context = context;
            _mailHelper = mailHelper;
        }

        public IActionResult Acil()
        {
            return View();
        }

        public IActionResult AcilDurumSayfa()
        {
            var acilDurum = _context.AcilDurumlar;
            return View(acilDurum);
        }

        public IActionResult AcilList()
        {
            var acilDurum = _context.AcilDurumlar;
            return View(acilDurum);
        }

        [HttpPost]
        public IActionResult Yayinla(string konu, string mesaj)
        {
            if (string.IsNullOrEmpty(konu) || string.IsNullOrEmpty(mesaj))
            {
                ViewBag.Mesaj = "Konu ve mesaj boş olamaz!";
                return View("Acil");
            }

            //veritabanına kaydet
            var acilDurum = new AcilDurum
            {
                Konu = konu,
                Aciklama = mesaj,
                Tarih = DateTime.Now,
                Saat = DateTime.Now.TimeOfDay
            };

            _context.AcilDurumlar.Add(acilDurum);
            _context.SaveChanges();

            // mail adreslerini al
            var emailList = _context.Hocalar
                .Select(h => h.Mail)
                .Concat(_context.Asistanlar.Select(a => a.Mail))
                .Where(email => !string.IsNullOrEmpty(email))
                .ToList();

            // gönderim
            foreach (var email in emailList)
            {
                _mailHelper.Gonder(email, "Acil Durum: " + konu, mesaj);
            }

            ViewBag.Mesaj = "Acil durum başarıyla yayınlandı, mail gönderildi ve kaydedildi.";
            return View("Acil");
        }

        public IActionResult AcilUpdate(int id)
        {
            // acil durum kaydını getir
            var acilDurum = _context.AcilDurumlar.FirstOrDefault(a => a.Id == id);
            if (acilDurum == null)
            {
                return NotFound();
            }
            return View(acilDurum);
        }

        // düzenleme
        [HttpPost]
        public IActionResult AcilUpdate(AcilDurum model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // kaydı bul
            var acilDurum = _context.AcilDurumlar.FirstOrDefault(a => a.Id == model.Id);
            if (acilDurum == null)
            {
                return NotFound();
            }

            // güncelle
            acilDurum.Konu = model.Konu;
            acilDurum.Aciklama = model.Aciklama;
            _context.SaveChanges();

            return RedirectToAction("AcilList");
        }

        public IActionResult AcilDelete(int id)
        {
            var acilDurum = _context.AcilDurumlar.FirstOrDefault(a => a.Id == id);
            if (acilDurum == null)
            {
                return NotFound();
            }
            return View(acilDurum);
        }

        [HttpPost, ActionName("AcilDelete")]
        public IActionResult DeleteConfirmed(int id)
        {           
            var acilDurum = _context.AcilDurumlar.FirstOrDefault(a => a.Id == id);
            if (acilDurum == null)
            {
                return NotFound();
            }

            _context.AcilDurumlar.Remove(acilDurum);
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Asistanlar', RESEED, 0)");

            return RedirectToAction("AcilList");
        }

    }
}
