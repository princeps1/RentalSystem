using Microsoft.AspNetCore.Identity;

namespace RentalSystem.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public required string Name { get; set; }
}
