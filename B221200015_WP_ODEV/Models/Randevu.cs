using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace B221200015_WP_ODEV.Models
{
    public class Randevu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key

        [ForeignKey("AsistanId")]
        [Required(ErrorMessage = "Asistan Adı alanı boş bırakılamaz.")]
        public int AsistanId { get; set; } // Foreign Key       
        public Asistan Asistan { get; set; } // Navigation property
       
        [ForeignKey("HocaId")]
        [Required(ErrorMessage = "Hoca Adı alanı boş bırakılamaz.")]
        public int HocaId { get; set; } // Foreign Key                                        // 
        public Hoca Hoca { get; set; } // Navigation property

        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime Tarih { get; set; } // DateTime, tarih için DateTime kullanılması daha doğru

        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan Saat { get; set; } // Saat için TimeSpan kullanılması daha doğru
    }

}
