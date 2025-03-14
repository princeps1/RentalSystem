namespace RentalSystem.Repositories.Interfaces;

public interface IKorisnikRepository
{
    public Task<Korisnik> DodajAsync(string imePrezime, string jmbg, string brVozacke);
    public Task<Korisnik> AzurirajVozackuAsync(string jmbg,string brVozacke);
    public Task<List<KorisnikDTO>> PrikaziSveAsync();
    public Task<KorisnikDTO?> PrikaziKorisnikaAsync(string jmbg);
    public Task ObrisiAsync(string jmbg);

    //HELPERI
    public Task<bool> DaLiPostojiAsync(string? jmbg = null, string? brVozacke = null);
    public Task<bool> KorisnikJeIznajmioVoziloAsync(string? jmbg = null);

}