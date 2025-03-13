namespace RentalSystem.Domain.DTOs.Auth;

public class UserDTO
{
    [Key]
    public string? ID { get; set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
}
