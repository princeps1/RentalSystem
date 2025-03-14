using Microsoft.AspNetCore.Identity;

namespace RentalSystem.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}
