using NoSolo.Abstractions.Repositories.Base;

namespace NoSolo.Abstractions.Data.Data;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<bool> Complete();
    bool HasChanges();
    new void Dispose();
}