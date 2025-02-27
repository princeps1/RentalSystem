namespace RentalSystem.Models
{
    public class Motor : Vozilo
    {
        public required string Vrsta { get; set; }

        protected override decimal UzmiManjuCenuRente() => 15m;
        protected override decimal UzmiVecuCenuRente() => 10m;
        protected override decimal UzmiProcenatOsiguranja() => 0.02m;
    }
}
