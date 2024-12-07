using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace B221200015_WP_ODEV.Controllers
{
    public class TakvimController : Controller
    {
        private readonly DatabaseContext _context;

        public TakvimController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: /Takvim
        public IActionResult Takvim()
        {
            return View();
        }

        // GET: /Takvim/GetEvents
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            // Veritabanından nöbet verilerini al
            var nobetler = await _context.Nobetler
                .Include(n => n.Asistan) // Asistan bilgisi için join
                .Include(n => n.Bolum) // Bölüm bilgisi için join
                .ToListAsync();

            // Nöbet bilgilerini FullCalendar'a uygun formata dönüştür
            var events = nobetler.Select(n => new
            {
                title = $"{n.Asistan.Ad} - {n.Bolum.BolumAdi}", // Asistan ve Bölüm adını birleştir
                start = n.BaslamaTarihi.Add(n.BaslamaSaati), // Başlama tarih + saat
                end = n.BitisTarihi.Add(n.BitisSaati) // Bitiş tarih + saat
            }).ToList();

            return Json(events); // JSON formatında döndür
        }

    }
}
