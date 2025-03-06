using Refit;
using RentalSystemUI.DTOs;

namespace RentalSystemUI.DataAccess;

public interface IUserApi
{
    [Post("/User/login")]
    Task<LoginResponseDTO> Login([Body] LoginRequestDTO request);

    [Post("/User/register")]
    Task<UserDTO> Register([Body] RegistrationRequestDTO request);
}
