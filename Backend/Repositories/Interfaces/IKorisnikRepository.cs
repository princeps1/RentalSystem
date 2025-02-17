namespace WebTemplate.Repositories.Interfaces;

public interface IKorisnikRepository
{
    public Task<Korisnik> DodajAsync(string imePrezime, string jmbg, int brVozacke);
    public Task<Korisnik> AzurirajVozackuAsync(string jmbg,int brVozacke);
    public Task<List<Korisnik>> PrikaziSveAsync();
    public Task<Korisnik?> PrikaziKorisnikaAsync(string jmbg);
    public Task ObrisiAsync(string jmbg);
    public Task<bool> DaLiPostoji(string? jmbg = null, int? brVozacke = 0);
    public Task<bool> KorisnikJeIznajmioVozilo(string? jmbg = null);

}