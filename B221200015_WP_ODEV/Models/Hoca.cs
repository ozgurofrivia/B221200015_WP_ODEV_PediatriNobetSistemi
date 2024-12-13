using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B221200015_WP_ODEV.Models
{
    public class Hoca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        [Required]
        public string Telefon { get; set; }
        [EmailAddress]
        public string Mail { get; set; }
        public int? BolumId { get; set; } // Foreign Key

        [ForeignKey("BolumId")]
        public Bolum Bolum { get; set; } // Navigation property

        public ICollection<Randevu> Randevular { get; set; } = new List<Randevu>();
        public ICollection<HocaMusaitlik> HocaMusaitlikler { get; set; } = new List<HocaMusaitlik>();

    }

}
