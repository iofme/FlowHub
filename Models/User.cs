using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser
    {
        public required DateTime Aniversario { get; set; }
        public required string Cargo { get; set; }
        public List<Card>? Cards { get; set; }
        public string? ImgUrl { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshRokenExpiry { get; set; }
    }
}