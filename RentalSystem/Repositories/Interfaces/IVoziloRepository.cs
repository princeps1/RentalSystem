namespace RentalSystem.Repositories.Interfaces;

public interface IVoziloRepository 
{

    public Task DodajAsync(VoziloDodavanjeDTO voziloDTO);
    public Task<List<VoziloDTO>> PrikaziSveAsync();
    public Task<VoziloDTO?> PrikaziVoziloAsync(string regBroj);
    public Task ObrisiAsync(string regBroj);




    //HELPERI
    public Task<bool> DaLiPostojiAsync(string? regBroj = null);
    public Task<bool> DaLiJeIznajmljeno(string regBr);

    public Task<Vozilo?> DohvatiVoziloAsync(string regBroj);

}