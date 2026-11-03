using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITodoRepository Todos { get; }
        Task<int> CommitAsync();
    }
}
