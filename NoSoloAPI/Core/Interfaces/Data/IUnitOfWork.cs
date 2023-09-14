using Core.Entities;
using Core.Interfaces.Repositories;

namespace Core.Interfaces.Data;

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