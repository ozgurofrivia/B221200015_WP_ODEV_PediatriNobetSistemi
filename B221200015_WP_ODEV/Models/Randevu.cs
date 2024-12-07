using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace B221200015_WP_ODEV.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [ForeignKey("AsistanId")]
        public int AsistanId { get; set; } // Foreign Key       
        public Asistan Asistan { get; set; } // Navigation property
        [ForeignKey("HocaId")]
        public int HocaId { get; set; } // Foreign Key      
        public Hoca Hoca { get; set; } // Navigation property
        [Required]
        public DateTime Tarih { get; set; } // DateTime, tarih için DateTime kullanılması daha doğru
        [Required]
        public TimeSpan Saat { get; set; } // Saat için TimeSpan kullanılması daha doğru
    }

}
