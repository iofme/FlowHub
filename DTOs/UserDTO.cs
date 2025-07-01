using API.Models;

namespace API.DTOs
{
    public class UserDTO
    {
        public required string UserName { get; set; }
        public required DateTime Aniversario { get; set; }
        public required string Cargo { get; set; }
        public required string Token { get; set; }
        public List<Card> Cards { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}