namespace WebTemplate.Models
{
    public class Motor : Vozilo
    {
        public required string Tip { get; set; }

        protected override decimal UzmiManjuCenuRente() => 15m;
        protected override decimal UzmiVecuCenuRente() => 10m;
        protected override decimal UzmiProcenatOsiguranja() => 0.02m;
    }
}
