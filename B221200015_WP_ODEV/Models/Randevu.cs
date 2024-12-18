using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace B221200015_WP_ODEV.Models
{
    public class Randevu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("AsistanId")]
        [Required(ErrorMessage = "Asistan Adı alanı boş bırakılamaz.")]
        public int AsistanId { get; set; } 
        
        public Asistan Asistan { get; set; }
       
        [ForeignKey("HocaId")]
        [Required(ErrorMessage = "Hoca Adı alanı boş bırakılamaz.")]
        public int HocaId { get; set; } 

        public Hoca Hoca { get; set; } 

        [Required(ErrorMessage = "Tarih alanı boş bırakılamaz.")]
        public DateTime Tarih { get; set; } 

        [Required(ErrorMessage = "Saat alanı boş bırakılamaz.")]
        public TimeSpan Saat { get; set; }
    }

}
