using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class User : IdentityUser<int>
    {
        public required DateTime Aniversario { get; set; }
        public required string Cargo { get; set; }
        public required List<Card> Cards { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}