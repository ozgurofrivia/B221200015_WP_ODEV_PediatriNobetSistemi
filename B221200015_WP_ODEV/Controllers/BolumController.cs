using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Models;
using System.Linq;
using B221200015_WP_ODEV.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace B221200015_WP_ODEV.Controllers
{
    public class BolumController : Controller
    {
        private readonly DatabaseContext _context;

        public BolumController(DatabaseContext context)
        {
            _context = context;

            if (!_context.Bolumler.Any())
            {
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Acil", HastaSayisi = 14, BosYatakSayisi = 12 });
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Yoğun Bakımı", HastaSayisi = 14, BosYatakSayisi = 12 });
                _context.Bolumler.Add(new Bolum() { BolumAdi = "Çocuk Hematolojisi ve Onkolojisi", HastaSayisi = 14, BosYatakSayisi = 12 });

                _context.SaveChanges();
            }
        }

        public IActionResult Bolum()
        {
            var bolumler = _context.Bolumler.ToList();
            return View(bolumler);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult BolumList()
        {
            var bolumler = _context.Bolumler.ToList();
            return View(bolumler);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult BolumAdd(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult BolumAdd(Bolum bolum)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(bolum);
            }

            _context.Bolumler.Add(bolum);
            _context.SaveChanges();
            return RedirectToAction("BolumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumUpdate(int id)
        {
            var bolum = _context.Bolumler.Find(id);
            if (bolum == null) return NotFound();
            return View(bolum);
        }

        
        [HttpPost]
        public IActionResult BolumUpdate(Bolum bolum)
        {
            _context.Bolumler.Update(bolum);
            _context.SaveChanges();
            return RedirectToAction("BolumList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BolumDelete(int id)
        {
            var bolum = _context.Bolumler
                .Include(b => b.Nobetler) 
                .Include(b => b.Asistanlar)
                .Include(b => b.Hocalar) 
                .FirstOrDefault(b => b.Id == id);

            if (bolum == null) return NotFound();
            return View(bolum);
        }


        [HttpPost, ActionName("BolumDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var bolum = _context.Bolumler
                .Include(b => b.Nobetler)
                .FirstOrDefault(b => b.Id == id);

            if (bolum != null)
            {
                if (bolum.Nobetler != null)
                {
                    _context.Nobetler.RemoveRange(bolum.Nobetler);
                }

                _context.Bolumler.Remove(bolum);
                _context.SaveChanges();
            }
            return RedirectToAction("BolumList");
        }


    }
}
