using B221200015_WP_ODEV.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Hasta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Ad { get; set; }

    [Required]
    public string Soyad { get; set; }

    public int Yas { get; set; }

    public string Durum { get; set; }

    public int BolumId { get; set; }

    public string YatisDurumu { get; set; }

    public DateTime YatisTarihi { get; set; } 

    public DateTime? CikisTarihi { get; set; } 

    [ForeignKey("BolumId")]
    public Bolum Bolum { get; set; }
}
