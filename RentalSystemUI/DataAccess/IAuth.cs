using Refit;
using RentalSystemUI.DTOs.Auth;

namespace RentalSystemUI.DataAccess;

public interface IAuth
{
    [Post("/User/login")]
    Task<LoginResponseDTO> Login([Body] LoginRequestDTO request);

    [Post("/User/register")]
    Task<UserDTO> Register([Body] RegistrationRequestDTO request);
}
