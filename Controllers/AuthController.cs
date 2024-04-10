using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seedium.Models.DTO;
using Seedium.Repositories.Interface;

namespace Seedium.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenRepository _jwtTokenRepository;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        UserManager<IdentityUser> userManager,
        IJwtTokenRepository jwtTokenRepository,
        ILogger<AuthController> logger
    )
    {
        _userManager = userManager;
        _jwtTokenRepository = jwtTokenRepository;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var user = new IdentityUser
        {
            UserName = request.Email?.Trim(),
            Email = request.Email?.Trim()
        };

        var userExists = await _userManager.FindByEmailAsync(user.Email!);
        if (userExists != null)
        {
            ModelState.AddModelError("", "User already registered");
            return ValidationProblem(ModelState);
        }

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        if (identityResult.Succeeded)
        {
            identityResult = await _userManager.AddToRoleAsync(user, "Reader");

            if (identityResult.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenRepository.GenerateJwtToken(user, [.. roles]);

                var registerResponseDto = request.Adapt<RegisterResponseDto>();
                registerResponseDto.Roles = [.. roles];
                registerResponseDto.Token = token;
                registerResponseDto.Email = user.Email!;

                _logger.LogInformation("register Response Dto : {@registerResponseDto}", registerResponseDto);
                return Ok(registerResponseDto);
            }
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
        }
        else
        {
            if (identityResult.Errors.Any())
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
        return ValidationProblem(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (checkPasswordResult)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var token = _jwtTokenRepository.GenerateJwtToken(user, [.. roles]);

                var loginResponseDto = request.Adapt<LoginResponseDto>();
                loginResponseDto.Roles = [.. roles];
                loginResponseDto.Token = token;
                loginResponseDto.Email = user.Email!;

                _logger.LogInformation("login Response Dto : {@registerResponseDto}", loginResponseDto);
                return Ok(loginResponseDto);
            }
        }

        ModelState.AddModelError("", "Invalid credentials");
        return ValidationProblem(ModelState);
    }
}
