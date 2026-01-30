using System;
using DatingApp_WebAPI.Entities;

namespace DatingApp_WebAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
