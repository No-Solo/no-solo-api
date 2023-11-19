using NoSolo.Core.Entities;
using NoSolo.Abstractions.Base;
using NoSolo.Abstractions.Repositories;

namespace NoSolo.Abstractions.Data;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IOrganizationRepository OrganizationRepository { get; }
    IRefreshTokenRepository RefreshTokenRepository { get; }
    IUserProfileRepository UserProfileRepository { get; }
    IUserTagRepository UserTagRepository { get; }
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
    Task<bool> Complete();
    bool HasChanges();
    void Dispose();
}