

using NoSolo.Abstractions.Base;
using NoSolo.Abstractions.Repositories.Repositories;

namespace NoSolo.Abstractions.Data.Data;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IOrganizationRepository OrganizationRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IUserProfileRepository UserProfileRepository { get; }
    IUserTagRepository UserTagRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<bool> Complete();
    bool HasChanges();
    void Dispose();
}