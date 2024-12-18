using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B221200015_WP_ODEV.Models
{
    public class Bolum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Bölüm Adı alanı boş bırakılamaz.")]
        public string BolumAdi { get; set; }

        public int? HastaSayisi { get; set; }

        public int? BosYatakSayisi { get; set; }

        public ICollection<Asistan>? Asistanlar { get; set; } 

        public ICollection<Hoca>? Hocalar { get; set; } 

        public ICollection<Hasta>? Hastalar { get; set; }

        public ICollection<Nobet>? Nobetler { get; set; }
    }
}
