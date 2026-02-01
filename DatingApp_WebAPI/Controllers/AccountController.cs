using System.Security.Cryptography;
using System.Text;
using DatingApp_WebAPI.Data;
using DatingApp_WebAPI.DTOs;
using DatingApp_WebAPI.Entities;
using DatingApp_WebAPI.Extensions;
using DatingApp_WebAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_WebAPI.Controllers
{
    public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
    {        
        [HttpPost("{register}")] //api/account/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await EmailExists(registerDTO.Email))
            {
                return BadRequest("Email is already taken");
            }

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                Email = registerDTO.Email,
                DisplayName = registerDTO.DisplayName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            
            return user.ToUserDTO(tokenService);
        }
        
        [HttpPost("login")] //api/account/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user =  await context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == loginDTO.Email.ToLower());
            if (user == null)
            {
                return Unauthorized("Invalid email");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid password");
                }
            }

            return user.ToUserDTO(tokenService);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}