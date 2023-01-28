using System.ComponentModel.DataAnnotations;

namespace Revas.Core.Entities;

public class Portfolio
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string? Image { get; set; }
}
