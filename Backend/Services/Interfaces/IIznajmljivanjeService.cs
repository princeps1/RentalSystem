namespace WebTemplate.Services.Interfaces;

public interface IIznajmljivanjeService
{
    public Task IznajmiVoziloAsync(int brDana, string regBroj,string jmbg);

    //HELPERI
    public Task<bool> DaLiKorisnikPostojiAsync(string jmbg);
    public Task<bool> DaLiVoziloPostojiAsync(string regBroj);
    public Task<bool> DaLiJeVoziloIznajmljeno(string regBr);



}
