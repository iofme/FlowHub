
using System.Text.Json.Serialization;

namespace API.Models
{
    public class List
    {
        public int Id { get; set; }
        public required string NomeLista { get; set; }
        public List<Card> Cards { get; set; } = [];
    }
}