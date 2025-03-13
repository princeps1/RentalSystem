using RentalSystem.Domain.DTOs.Auth;

namespace RentalSystem.Repositories.Interfaces;

public interface IUserRepository
{
    Task<LoginResponseDTO> Login(LoginRequestDTO model);
    Task<UserDTO> Register(RegistrationRequestDTO model);
    bool IsUniqueUser(string userName);
}
