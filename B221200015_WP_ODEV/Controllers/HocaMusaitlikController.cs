using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B221200015_WP_ODEV.Models;
using B221200015_WP_ODEV.Data;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace B221200015_WP_ODEV.Controllers
{
    public class HocaMusaitlikController : Controller
    {
        private readonly DatabaseContext _context;

        public HocaMusaitlikController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult HocaMusaitlik(string searchTerm)
        {
            var hocaMusaitlikler = _context.HocaMusaitlikler
                .Include(hm => hm.Hoca)
                .OrderBy(hm => hm.Hoca.Ad)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocaMusaitlikler = hocaMusaitlikler.Where(hm =>
                    hm.Hoca.Ad.Contains(searchTerm) ||
                    hm.Hoca.Soyad.Contains(searchTerm) ||
                    (hm.Hoca.Ad + " " + hm.Hoca.Soyad).Contains(searchTerm));
            }

            return View(hocaMusaitlikler.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult HocaMusaitlikList(string searchTerm)
        {
            var hocaMusaitlikler = _context.HocaMusaitlikler
                .Include(hm => hm.Hoca)
                .OrderBy(hm => hm.Hoca.Ad)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                hocaMusaitlikler = hocaMusaitlikler.Where(hm =>
                    hm.Hoca.Ad.Contains(searchTerm) ||
                    hm.Hoca.Soyad.Contains(searchTerm) ||
                    (hm.Hoca.Ad + " " + hm.Hoca.Soyad).Contains(searchTerm));
            }

            return View(hocaMusaitlikler.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaMusaitlikAdd()
        {
            ViewBag.Hocalar = _context.Hocalar.ToList();
            return View();
        }
    
        [HttpPost]
        public IActionResult HocaMusaitlikAdd(HocaMusaitlik musaitlik)
        {
           
            _context.HocaMusaitlikler.Add(musaitlik);
            _context.SaveChanges();

            ViewBag.Hocalar = _context.Hocalar.ToList();
            return RedirectToAction("HocaMusaitlikList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaMusaitlikUpdate(int id)
        {
            var musaitlik = _context.HocaMusaitlikler.Find(id);
            if (musaitlik == null) return NotFound();

            ViewBag.Hocalar = _context.Hocalar.ToList();
            return View(musaitlik);
        }

        [HttpPost]
        public IActionResult HocaMusaitlikUpdate(HocaMusaitlik musaitlik)
        {
            
            _context.HocaMusaitlikler.Update(musaitlik);
            _context.SaveChanges();       
            
            ViewBag.Hocalar = _context.Hocalar.ToList();
            return RedirectToAction("HocaMusaitlikList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult HocaMusaitlikDelete(int id)
        {
            var musaitlik = _context.HocaMusaitlikler.Include(h => h.Hoca).FirstOrDefault(m => m.Id == id);
            if (musaitlik == null) return NotFound();

            return View(musaitlik);
        }

        [HttpPost, ActionName("HocaMusaitlikDelete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var musaitlik = _context.HocaMusaitlikler.Find(id);
            if (musaitlik == null) return NotFound();
            _context.HocaMusaitlikler.Remove(musaitlik);
            _context.SaveChanges();
            return RedirectToAction("HocaMusaitlikList");
        }
    }
}
