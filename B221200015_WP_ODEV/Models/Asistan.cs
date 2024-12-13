using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace B221200015_WP_ODEV.Models
{
    public class Asistan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Primary Key
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        [EmailAddress]
        public string Mail { get; set; }

        public int? BolumId { get; set; } // Foreign Key - Nullable olmalı

        [ForeignKey("BolumId")]
        public Bolum Bolum { get; set; } // Navigation property
        public ICollection<Nobet> Nobetler { get; set; } // Navigation property for relationship with Nobet
        public ICollection<Randevu> Randevular { get; set; } // Navigation property for relationship with Randevu
    }


}
