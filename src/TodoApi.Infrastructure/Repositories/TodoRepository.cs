using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Enumerators;
using TodoApi.Domain.Interfaces;

namespace TodoApi.Infrastructure.Repositories
{
    public class TodoRepository(AppDbContext db) : ITodoRepository
    {
        public async Task<TodoItem?> GetByIdAsync(int id) => await db.Todos.FindAsync(id);

        public async Task<IEnumerable<TodoItem>> GetAllAsync(TodoStatus? status, DateTime? from, DateTime? to)
        {
            var query = db.Todos.AsNoTracking().AsQueryable();
            if (status.HasValue) query = query.Where(t => t.Status == status);
            if (from.HasValue) query = query.Where(t => t.DueDate >= from);
            if (to.HasValue) query = query.Where(t => t.DueDate <= to);
            return await query.OrderBy(t => t.DueDate).ToListAsync();
        }

        public async Task AddAsync(TodoItem item) => await db.Todos.AddAsync(item);
        public void Update(TodoItem item) => db.Todos.Update(item);
        public void Remove(TodoItem item) => db.Todos.Remove(item);
    }
}
