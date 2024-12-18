using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B221200015_WP_ODEV.Models
{
    public class Hoca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

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
        public int? BolumId { get; set; } 
        
        public Bolum? Bolum { get; set; } 

        public ICollection<Randevu>? Randevular { get; set; } = new List<Randevu>();

        public ICollection<HocaMusaitlik>? HocaMusaitlikler { get; set; } = new List<HocaMusaitlik>();

        public string? Resim { get; set; }
    }

}
