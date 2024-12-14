using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace B221200015_WP_ODEV.Models
{
    public class Asistan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string Soyad { get; set; }
        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz.")]
        public string Telefon { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        public string Mail { get; set; }
        [ForeignKey("BolumId")]
        public int? BolumId { get; set; } // Foreign Key - Nullable olmalı
        public Bolum? Bolum { get; set; }

        public ICollection<Nobet>? Nobetler { get; set; } // Navigation property for relationship with Nobet
        public ICollection<Randevu>? Randevular { get; set; } // Navigation property for relationship with Randevu

        // Yeni özellik: Resim yolu
        public string? Resim { get; set; }
    }
}
