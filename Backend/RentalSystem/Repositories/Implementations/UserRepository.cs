
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentalSystem.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RentalSystem.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly Context _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private string secretKey;
    private readonly IMapper _mapper;
    public UserRepository(Context context,IConfiguration conf,UserManager<ApplicationUser> userManager,IMapper mapper,RoleManager<IdentityRole> rolemanager)
    {
        _context = context;
        _userManager = userManager;
        secretKey = conf["ApiSettings:Secret"]!;
        _mapper = mapper;
        _roleManager = rolemanager;
    }
    public bool IsUniqueUser(string userName)
    {
        var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == userName);
        if(user == null)
        {
            return true;
        }
        return false;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
    {
        var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == model.UserName);

        bool isValid = await _userManager.CheckPasswordAsync(user!, model.Password);

        if(user == null || !isValid)
        {
            return new LoginResponseDTO
            {
                User = null!,
                Token = ""
            };
        }

        //Generisanje tokena
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);
        var roles = await _userManager.GetRolesAsync(user);

        var role = roles.FirstOrDefault();
        if (role == null)
        {
            
            role = "admin";
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, role)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };


        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponseDTO loginResponseDto = new LoginResponseDTO()
        {
            User = _mapper.Map<UserDTO>(user),
            Token = tokenHandler.WriteToken(token),
            Role = roles.FirstOrDefault()!
        };
        return loginResponseDto;
    }

    public async Task<UserDTO> Register(RegistrationRequestDTO model)
    {
        ApplicationUser user = new ()
        {
            UserName = model.UserName,
            Email = model.UserName,
            Name = model.Name
        };

        try
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()){
                    await _roleManager.CreateAsync(new IdentityRole("admin"));
                }
                await _userManager.AddToRoleAsync(user, "admin");
                var userToReturn = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.UserName == model.UserName);
                return _mapper.Map<UserDTO>(userToReturn);
            }
        }
        catch (Exception)
        {
        }
        return new UserDTO();
    }

  
}
