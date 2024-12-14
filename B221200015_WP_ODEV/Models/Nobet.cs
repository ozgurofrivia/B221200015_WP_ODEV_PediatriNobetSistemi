using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace B221200015_WP_ODEV.Models
{
    public class Nobet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [ForeignKey("AsistanId")]
        [Required(ErrorMessage = "Asistan alanı boş bırakılamaz.")]
        public int AsistanId { get; set; } // Foreign Key
        public Asistan Asistan { get; set; } // Navigation property
        [ForeignKey("BolumId")]
        [Required(ErrorMessage = "Bölüm alanı boş bırakılamaz.")]
        public int BolumId { get; set; } // Foreign Key
        public Bolum Bolum { get; set; } // Navigation property
        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan BaslamaSaati { get; set; } // Saat için TimeSpan kullanılması daha doğru
        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan BitisSaati { get; set; } // DateTime, saat bilgisi için DateTime kullanılmalı
        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime BaslamaTarihi { get; set; } // DateTime, tarih için DateTime kullanılması daha doğru
        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime BitisTarihi { get; set; } // DateTime, tarih için DateTime kullanılması daha doğru

    }
}
