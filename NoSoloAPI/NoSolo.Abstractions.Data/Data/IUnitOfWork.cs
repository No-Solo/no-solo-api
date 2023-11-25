using NoSolo.Abstractions.Repositories.Base;
using NoSolo.Abstractions.Repositories.Utility;

namespace NoSolo.Abstractions.Data.Data;

public interface IUnitOfWork : IDisposable
{
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<bool> Complete();
    bool HasChanges();
    void Dispose();
}