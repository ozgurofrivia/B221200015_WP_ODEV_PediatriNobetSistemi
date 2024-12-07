using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Models;
using B221200015_WP_ODEV.Helper;
using B221200015_WP_ODEV.Data;
using System.Linq;

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

        [HttpPost]
        public IActionResult Yayinla(string mesaj)
        {
            if (string.IsNullOrEmpty(mesaj))
            {
                ViewBag.Mesaj = "Mesaj boş olamaz!";
                return View("Acil");
            }

            // Acil durumu veritabanına kaydet
            var acilDurum = new AcilDurum
            {
                Durum = "Acil Durum",
                Aciklama = mesaj
            };

            _context.AcilDurumlar.Add(acilDurum);
            _context.SaveChanges();

            // E-posta adreslerini al
            var emailList = _context.Hocalar
                .Select(h => h.Mail)
                .Concat(_context.Asistanlar.Select(a => a.Mail))
                .Where(email => !string.IsNullOrEmpty(email))
                .ToList();

            // Mail gönderimi
            foreach (var email in emailList)
            {
                _mailHelper.Gonder(email, "Acil Durum", mesaj);
            }

            ViewBag.Mesaj = "Acil durum başarıyla yayınlandı, mail gönderildi ve kaydedildi.";
            return View("Acil");
        }
    }
}
