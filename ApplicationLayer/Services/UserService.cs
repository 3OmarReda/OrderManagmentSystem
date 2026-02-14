using ApplicationLayer.Dtos.User;
using ApplicationLayer.Interfaces;
using ApplicationLayer.ResultPattern;
using DataAccessLayer.Data.Models;
using DataAccessLayer.Data.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<Result> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = UserRole.Customer 
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return Result.Failure(new Error(ErrorCode.InvalidData, string.Join(", ", result.Errors.Select(e => e.Description))));

            return Result.Success();
        }

        public async Task<ResultT<string>> LoginAsync(DtoLogin dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user is null)
                return ResultT<string>.Failure(new Error(ErrorCode.NotFound, "User not found"));

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return ResultT<string>.Failure(new Error(ErrorCode.InvalidData, "Invalid password"));

            // إنشاء JWT
            var token = GenerateJwtToken(user);
            return ResultT<string>.Success(token);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
