namespace WebTemplate.Models;

public class Korisnik
{
    [Key]
    public int ID { get; set; }

    public required string ImePrezime { get; set; }

    public required string JMBG { get; set; }

    public int BrVozacke { get; set; }

    public List<Vozilo>? Vozila { get; set; }

    public Korisnik()
    {
        Vozila = new List<Vozilo>();
    }
}

