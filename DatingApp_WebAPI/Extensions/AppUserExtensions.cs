using DatingApp_WebAPI.DTOs;
using DatingApp_WebAPI.Entities;
using DatingApp_WebAPI.Interfaces;

namespace DatingApp_WebAPI.Extensions;

public static class AppUserExtensions
{
    public static UserDTO ToUserDTO(this AppUser user, ITokenService tokenService)
    {
        return new UserDTO
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user)
        };
    }
}
