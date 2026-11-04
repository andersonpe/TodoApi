namespace TodoApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITodoRepository Todos { get; }
        Task<int> CommitAsync();
    }
}
