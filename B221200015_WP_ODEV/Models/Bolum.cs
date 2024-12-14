using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B221200015_WP_ODEV.Models
{
    public class Bolum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [Required(ErrorMessage = "Bölüm Adı alanı boş bırakılamaz.")]
        public string BolumAdi { get; set; }
        public int? HastaSayisi { get; set; }
        public int? BosYatakSayisi { get; set; }
        public ICollection<Asistan>? Asistanlar { get; set; } // Navigation property for relationship with Asistan
        public ICollection<Hoca>? Hocalar { get; set; } // Navigation property for relationship with Hoca
        public ICollection<Hasta>? Hastalar { get; set; } // Navigation property for relationship with Hasta
        public ICollection<Nobet>? Nobetler { get; set; } // Navigation property for relationship with Hoca
    }
}
