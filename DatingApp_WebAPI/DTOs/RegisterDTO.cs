using System.ComponentModel.DataAnnotations;

namespace DatingApp_WebAPI.DTOs;

public class RegisterDTO
{
    [Required]
    public string DisplayName { get; set; }="";

    [Required]
    [EmailAddress]
    public string Email { get; set; }="";

    [Required]
    [MinLength(5)]
    public string Password { get; set; }="";
}
