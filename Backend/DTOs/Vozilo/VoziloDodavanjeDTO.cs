namespace RentalSystem.DTOs.Vozilo;

public abstract class VoziloDodavanjeDTO
{
    public required string Tip{ get; set; }
    public required string Marka { get; set; }
    public required string Model { get; set; }

    public required string RegistarskiBroj { get; set; }

    public int PredjenoKm { get; set; }

    public int Godiste { get; set; }

    public int CenaVozila { get; set; }

    public bool Iznajmljen { get; set; } = false;

    public int? BrDanaIznajmljivanja { get; set; } = 0;

    public string? ImePrezimeKorisnika { get; set; }
}

public class AutomobilDodavanjeDTO : VoziloDodavanjeDTO
{
    public int BrSedista { get; set; }
    public required string Gorivo { get; set; }
    public required string Karoserija { get; set; }
}

public class MotorDodavanjeDTO : VoziloDodavanjeDTO
{
    public required string Vrsta { get; set; }
}