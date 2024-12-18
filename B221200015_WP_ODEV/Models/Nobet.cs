using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace B221200015_WP_ODEV.Models
{
    public class Nobet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AsistanId")]
        [Required(ErrorMessage = "Asistan alanı boş bırakılamaz.")]
        public int AsistanId { get; set; } 

        public Asistan Asistan { get; set; } 

        [ForeignKey("BolumId")]
        [Required(ErrorMessage = "Bölüm alanı boş bırakılamaz.")]
        public int BolumId { get; set; }

        public Bolum Bolum { get; set; }

        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan BaslamaSaati { get; set; }

        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan BitisSaati { get; set; }

        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime BaslamaTarihi { get; set; }

        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime BitisTarihi { get; set; }
    }
}
