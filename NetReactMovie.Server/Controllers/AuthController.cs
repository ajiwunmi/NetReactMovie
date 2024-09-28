using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetReactMovie.Server.Data.DTO;
using NetReactMovie.Server.Models.Entities;
using NetReactMovie.Server.Services.Interfaces;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] SignupUserDto dto)
    {
        if (dto.Password != dto.Password_confirmation)
            return BadRequest("Password and confirmation do not match.");

        var user = new User()
        {
            UserName = dto.Email,
            Email = dto.Email,
            Name = dto.Name
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        var token = _jwtService.GenerateToken(user);
        return Ok(new { message = "User registered successfully", email = user.Email,  name= user.Name , token = token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid login attempt.");
        }

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            return NotFound();
        }
        var token = _jwtService.GenerateToken(user);

        return Ok(new { token });
    }
}
