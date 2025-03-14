namespace RentalSystemUI.DTOs.Vozilo
{
    public class VoziloMinimumDTO
    {
        public required string Marka { get; set; }
        public required string Model { get; set; }
        public required string RegistarskiBroj { get; set; }
        public int CenaVozila { get; set; }

    }
}
