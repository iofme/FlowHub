using API.Models;

namespace API.DTOs
{
    public class ListDTO
    {
        public required string NomeLista { get; set; }
        public required List<Card> Cards { get; set; }
    }
}