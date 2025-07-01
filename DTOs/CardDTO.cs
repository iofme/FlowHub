
namespace API.DTOs
{
    public class CardDTO
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required string CriadoPor { get; set; }
        public required string Atribuido { get; set; }
        public required DateTime DataDeFinalizacao { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.Now;
    }
}