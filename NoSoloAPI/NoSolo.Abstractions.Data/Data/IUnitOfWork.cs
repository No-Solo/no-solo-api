using NoSolo.Abstractions.Repositories.Base;

namespace NoSolo.Abstractions.Data.Data;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<bool> Complete();
    bool HasChanges();
}