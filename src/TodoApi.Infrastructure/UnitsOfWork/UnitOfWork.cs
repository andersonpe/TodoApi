using TodoApi.Domain.Interfaces;
using TodoApi.Infrastructure.Repositories;

namespace TodoApi.Infrastructure.UnitsOfWork
{
    public class UnitOfWork(AppDbContext db) : IUnitOfWork
    {
        public ITodoRepository Todos { get; } = new TodoRepository(db);
        public Task<int> CommitAsync() => db.SaveChangesAsync();
    }
}
