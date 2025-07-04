using API.Models;

namespace API.DTOs;

public class GroupDTO
{
        public required string DisplayName { get; set; }
        public List<List>? Listas { get; set; }
        public List<User>? User { get; set; }
}
