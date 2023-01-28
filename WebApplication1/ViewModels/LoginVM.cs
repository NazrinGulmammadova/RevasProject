using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class LoginVM
{
    [MaxLength(100), DataType(DataType.EmailAddress)]
    public string? UsernameOrEmail { get; set; }
    [MaxLength(100), DataType(DataType.Password)]
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}
