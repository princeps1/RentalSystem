using System.Reflection.Metadata.Ecma335;

namespace RentalSystem.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    public UserController(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LognAsync([FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _userRepo.Login(model);
        if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
        {
            return BadRequest();
        }
        return Ok(loginResponse);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestDTO model)
    {
        bool ifUserNameUnique =  _userRepo.IsUniqueUser(model.UserName);
        if(!ifUserNameUnique)
        {
            return BadRequest("Vec postoji korisnik sa ovim username ili password");
        }
        var user = await _userRepo.Register(model);
        if(user == null)
        {
           return BadRequest();
        }
        return Ok(user);
    }
}
