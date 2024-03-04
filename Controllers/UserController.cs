using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using API.Models;
using API.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly string _jwtSecret;
    private readonly int _jwtExpirationInMinutes;

    public UserController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _jwtSecret = configuration["Jwt:Secret"];
        _jwtExpirationInMinutes = int.Parse(configuration["Jwt:ExpirationInMinutes"]);
    }

    [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    return BadRequest(new { message = "Email is already registered" });
                }

                var user = new UserModel { Email = model.Email, Password = model.Password };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new { message = "User registered successfully" });
            }

            return BadRequest(ModelState);
        }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        if (user.Password != model.Password)
        {
            return BadRequest(new { message = "Incorrect password" });
        }

        var token = GenerateJwtToken(user);
        return Ok(new { token, user.Email });
    }

    private string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
            new System.Security.Claims.Claim(ClaimTypes.Email, user.Id.ToString()),
            new System.Security.Claims.Claim(ClaimTypes.Email, user.Email)
        }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "User logged out successfully" });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }
}
