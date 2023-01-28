using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels;

public class RegisterVM
{
    [MaxLength(100)]
    public string? Username { get; set; }
    [MaxLength(100), DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [MaxLength(100), DataType(DataType.Password)]
    public string? Password { get; set; }
    [MaxLength(100), DataType(DataType.Password), Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}
