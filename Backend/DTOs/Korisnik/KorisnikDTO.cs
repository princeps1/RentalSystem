namespace WebTemplate.DTOs.Korisnik
{
    public class KorisnikDTO
    {
        public required string ImePrezime { get; set; }

        public required string JMBG { get; set; }

        public required string BrVozacke { get; set; }

        public List<VoziloMinimumDTO>? Vozila { get; set; }
    }
}
