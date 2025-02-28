namespace RentalSystem.Domain.Models;

public class LocalUser
{
    public int Id { get; set; }
    public string  UsernName { get; set; }
    public string  Name { get; set; }
    public string Password { get; set; }

    public string Role { get; set; }

}
