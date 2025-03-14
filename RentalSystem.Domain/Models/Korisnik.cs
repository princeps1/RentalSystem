using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Domain.Models;

public class Korisnik
{
    [Key]
    public int ID { get; set; }

    public required string ImePrezime { get; set; }

    public required string JMBG { get; set; }

    public required string BrVozacke { get; set; }

    public List<Vozilo>? Vozila { get; set; }

    public Korisnik()
    {
        Vozila = new List<Vozilo>();
    }
}

