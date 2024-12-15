﻿using Microsoft.AspNetCore.Mvc;
using B221200015_WP_ODEV.Data;
using B221200015_WP_ODEV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace B221200015_WP_ODEV.Controllers
{
    public class AsistanController : Controller
    {
        private readonly DatabaseContext _context;

        public AsistanController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Asistan(string searchTerm, string sortOrder)
        {
            // Asistan bilgilerini bölümleriyle birlikte sorgulama
            var asistanlar = _context.Asistanlar
                .Include(h => h.Bolum)
                .AsQueryable();

            // Eğer arama terimi sağlanmışsa, filtreleme uygula
            if (!string.IsNullOrEmpty(searchTerm))
            {
                asistanlar = asistanlar.Where(h =>
                    h.Ad.Contains(searchTerm) ||
                    h.Soyad.Contains(searchTerm) ||
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            // Sıralama işlemi
            switch (sortOrder)
            {
                case "name_desc":
                    asistanlar = asistanlar.OrderByDescending(h => h.Ad);
                    break;
                case "department":
                    asistanlar = asistanlar.OrderBy(h => h.Bolum.BolumAdi);
                    break;
                case "department_desc":
                    asistanlar = asistanlar.OrderByDescending(h => h.Bolum.BolumAdi);
                    break;
                default:
                    asistanlar = asistanlar.OrderBy(h => h.Ad); // Varsayılan sıralama: ad'a göre
                    break;
            }

            // Sonuçları listeye çevir ve görünümde göster
            return View(asistanlar.ToList());
        }

        // Asistanların Listelenmesi
        public IActionResult AsistanList(string searchTerm)
        {
            var asistanlar = _context.Asistanlar
                .Include(h => h.Bolum)
                .OrderBy(h => h.Bolum.BolumAdi)
                .AsQueryable();

            // Arama fonksiyonu
            if (!string.IsNullOrEmpty(searchTerm))
            {
                asistanlar = asistanlar.Where(h =>
                    h.Ad.Contains(searchTerm) ||
                    h.Soyad.Contains(searchTerm) ||
                    (h.Ad + " " + h.Soyad).Contains(searchTerm)
                );
            }

            return View(asistanlar.ToList());
        }

        // Yeni Asistan Ekleme - GET
        [HttpGet]
        public IActionResult AsistanAdd()
        {
            // Bölüm listesini ViewBag ile gönderiyoruz
            ViewBag.Bolumler = _context.Bolumler.ToList();
            return View();
        }

        // Yeni Asistan Ekleme - POST
        [HttpPost]
        public IActionResult AsistanAdd(Asistan asistan, IFormFile Resim)
        {
            // Resim kaydetme işlemi
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "asistanlar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                asistan.Resim = "/images/asistanlar/" + Resim.FileName;
            }

            // Eğer ModelState geçerli değilse, formu tekrar gösteriyoruz
            if (!ModelState.IsValid)
            {
                // Bölüm listesini ViewBag ile tekrar göndermek
                ViewBag.Bolumler = _context.Bolumler.ToList();
                return View(asistan);
            }

            // Yeni asistanı ekliyoruz
            _context.Asistanlar.Add(asistan);
            _context.SaveChanges();

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
        public IActionResult AsistanUpdate(Asistan asistan, IFormFile Resim)
        {
            // Resim varsa, resmi kaydediyoruz
            if (Resim != null)
            {
                var resimYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "asistanlar", Resim.FileName);
                using (var fileStream = new FileStream(resimYolu, FileMode.Create))
                {
                    Resim.CopyTo(fileStream);
                }

                asistan.Resim = "/images/asistanlar/" + Resim.FileName;
            }

            // Asistan bilgilerini güncelliyoruz
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
                .Include(h => h.Randevular)
                .FirstOrDefault(h => h.Id == id);

            // Asistan varsa, randevuları ve asistanı siliyoruz
            if (asistan != null)
            {
                _context.Randevular.RemoveRange(asistan.Randevular);
                _context.Asistanlar.Remove(asistan);
                _context.SaveChanges();
            }

            return RedirectToAction("AsistanList");
        }
    }
}
