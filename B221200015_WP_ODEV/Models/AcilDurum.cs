using System.ComponentModel.DataAnnotations;

namespace B221200015_WP_ODEV.Models
{
    public class AcilDurum
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Durum { get; set; }

        public string Aciklama { get; set; }
    }
}
