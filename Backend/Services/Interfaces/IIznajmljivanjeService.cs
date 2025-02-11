namespace WebTemplate.Services.Interfaces;

public interface IIznajmljivanjeService
{
    public Task<ActionResult> IznajmiVoziloAsync(int brDana, int idVozila,int idKorisnika);
}
