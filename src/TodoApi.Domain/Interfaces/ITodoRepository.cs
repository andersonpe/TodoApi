using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;

namespace TodoApi.Domain.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem?> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync(TodoStatus? status, DateTime? dueFrom, DateTime? dueTo);
        Task AddAsync(TodoItem item);
        void Update(TodoItem item);
        void Remove(TodoItem item);
    }
}
