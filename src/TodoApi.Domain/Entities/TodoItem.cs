using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Domain.Enumerators;

namespace TodoApi.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Pendente;
        public DateTime DueDate { get; set; }
    }
}
