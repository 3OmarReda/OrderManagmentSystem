using ApplicationLayer.Dtos.User;
using ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Validation.User;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto is null)
                return BadRequest("User data is required.");

            var validator = new NewUserValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Invalid User Data", Errors = errors });
            }

            var result = await _userService.RegisterAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Message = "User registered successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DtoLogin dto)
        {
            if (dto is null)
                return BadRequest("Login data is required.");

            var validator = new LoginValidator();
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { Message = "Invalid Login Data", Errors = errors });
            }

            var result = await _userService.LoginAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result.Error.Message);

            return Ok(new { Token = result.Data, Message = "Login successful!" });
        }
    }
}
