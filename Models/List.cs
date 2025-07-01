
namespace API.Models
{
    public class List
    {
        public int Id { get; set; }
        public required string NomeLista { get; set; }
        public required List<Card> Cards { get; set; }
    }
}