namespace RentalSystem.Models;

public class Automobil : Vozilo
{


    public int BrSedista { get; set; }
    public required string Gorivo { get; set; }
    public required string Karoserija { get; set; }

    protected override decimal UzmiManjuCenuRente() => 20m;
    protected override decimal UzmiVecuCenuRente() => 15m;
    protected override decimal UzmiProcenatOsiguranja() => 0.01m;


}