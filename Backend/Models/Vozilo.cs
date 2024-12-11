namespace WebTemplate.Models
{
    public abstract class Vozilo
    {
        [Key]
        public int ID { get; set; }

        public required string Marka { get; set; }

        public required string RegistarskiBroj { get; set; }

        public required string Model { get; set; }

        public int PredjenoKm { get; set; }

        public int Godiste { get; set; }

        public int CenaVozila { get; set; }

        public bool Iznajmljen { get; set; } = false;

        public int? BrDanaIznajmljivanja { get; set; } = 0;

        protected abstract decimal UzmiManjuCenuRente();
        protected abstract decimal UzmiVecuCenuRente();
        protected abstract decimal UzmiProcenatOsiguranja();

        public List<Korisnik>? Korisnici { get; set; }


    }
}
