using System.Text.Json.Serialization;
using API.Models;

namespace API.DTOs
{
    public class ListDTO
    {
        public required string NomeLista { get; set; }

        public List<Card> Cards { get; set; } = [];
    }
}