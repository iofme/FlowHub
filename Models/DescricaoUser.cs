using System;

namespace API.Models;

public class DescricaoUser
{
    public int Id { get; set; }
    public DateTime DataCriada { get; set; } = DateTime.UtcNow;
    public required string TextAboutUser { get; set; }
}
