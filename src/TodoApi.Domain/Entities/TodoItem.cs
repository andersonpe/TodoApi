using TodoApi.Domain.Enumerators;

namespace TodoApi.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pendente;
        public DateTime DataDeVencimento { get; set; }
    }
}
