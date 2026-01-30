using System;

namespace DatingApp_WebAPI.DTOs;

public class LoginDTO
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}
